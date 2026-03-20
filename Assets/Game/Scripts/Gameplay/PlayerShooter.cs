using Game.Core;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private Transform _shootOrigin;
        [SerializeField] private ParticleSystem _muzzleFlash;
        [SerializeField] private float _shootRange = 100f;

        [Inject] private readonly IInputProvider _inputProvider;
        [Inject] private readonly PlayerModel _playerModel;

        private void Awake()
        {
            _inputProvider.ShootInput()
                .Subscribe(_ => Shoot())
                .AddTo(this);
        }

        private void Shoot()
        {
            Debug.Log("Shoot");
            if (_muzzleFlash != null) _muzzleFlash.Play();

            if (Physics.Raycast(_shootOrigin.position, _shootOrigin.forward, out RaycastHit hit, _shootRange))
            {
                if (hit.collider.TryGetComponent<EnemyHealth>(out var enemy))
                {
                    enemy.TakeDamage(_playerModel.Damage.Value, hit);
                }
            }
        }
    }
}
