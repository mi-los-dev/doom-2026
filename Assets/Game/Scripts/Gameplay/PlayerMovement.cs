using Game.Core;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _mouseSensitivity = 2f;
        [SerializeField] private float _verticalClamp = 80f;
        [SerializeField] private float _jumpForce = 5f;

        [Header("Ground Check")]
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private float _groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask _groundLayer;

        [Inject] private readonly IInputProvider _inputProvider;
        [Inject] private readonly PlayerModel _playerModel;

        private Rigidbody _rigidbody;
        private Vector2 _moveInput;
        private float _cameraPitch;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.freezeRotation = true;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _inputProvider.MoveInput()
                .Subscribe(input => _moveInput = input)
                .AddTo(this);

            _inputProvider.LookInput()
                .Subscribe(ApplyLook)
                .AddTo(this);

            _inputProvider.JumpInput()
                .Where(_ => IsGrounded())
                .Subscribe(ApplyJump)
                .AddTo(this);
        }

        private void FixedUpdate()
        {
            ApplyMovement();
        }

        private void ApplyMovement()
        {
            var direction = transform.right * _moveInput.x + transform.forward * _moveInput.y;
            var targetVelocity = direction * _playerModel.MoveSpeed.Value;
            targetVelocity.y = _rigidbody.velocity.y;
            _rigidbody.velocity = targetVelocity;
        }

        private void ApplyJump(Unit unit)
        {
            var direction = transform.right * _moveInput.x + transform.forward * _moveInput.y;
            var targetVelocity = direction * _playerModel.MoveSpeed.Value;
            targetVelocity.y = _rigidbody.velocity.y;
            _rigidbody.velocity = targetVelocity;
        }

        private void ApplyLook(Vector2 lookDelta)
        {
            transform.Rotate(Vector3.up, lookDelta.x * _mouseSensitivity);

            _cameraPitch -= lookDelta.y * _mouseSensitivity;
            _cameraPitch = Mathf.Clamp(_cameraPitch, -_verticalClamp, _verticalClamp);
            _cameraTransform.localEulerAngles = new Vector3(_cameraPitch, 0f, 0f);
        }

        private bool IsGrounded()
        {
            return Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _groundLayer);
        }
    }
}
