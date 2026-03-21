using System;
using Game.Core;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _shootOrigin;
        [SerializeField] private ParticleSystem _muzzleFlash;
        [SerializeField] private float _shootRange = 100f;
        [SerializeField] private LineRenderer[] _lazerLines;
        [SerializeField] private AudioSource _shootAudioSource;
        [SerializeField] private float _lazerDuration = 0.08f;

        [Inject] private readonly IInputProvider _inputProvider;
        [Inject] private readonly PlayerModel _playerModel;

        private int _lazerIndex;

        private void Awake()
        {
            if (_camera == null) _camera = Camera.main;

            foreach (var line in _lazerLines)
                line.enabled = false;

            _inputProvider.ShootInput()
                .Subscribe(_ => Shoot())
                .AddTo(this);
        }

        private void Shoot()
        {
            _muzzleFlash.Play();
            _shootAudioSource.Play();

            var ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            Vector3 hitPoint;
            if (Physics.Raycast(ray, out RaycastHit hit, _shootRange))
            {
                hitPoint = hit.point;
                if (hit.collider.TryGetComponent<EnemyHealth>(out var enemy))
                {
                    enemy.TakeDamage(new ShotInfotmation
                    {
                        Damage = _playerModel.Damage.Value,
                        HitPoint = hitPoint,
                        HitDirection = ray.direction
                    });
                }
            }
            else
            {
                hitPoint = ray.origin + ray.direction * _shootRange;
            }

            ShowLaserLine(_shootOrigin.position, hitPoint);
        }

        private void ShowLaserLine(Vector3 start, Vector3 end)
        {
            var line = _lazerLines[_lazerIndex];
            _lazerIndex = (_lazerIndex + 1) % _lazerLines.Length;

            line.useWorldSpace = true;
            line.SetPosition(0, start);
            line.SetPosition(1, end);
            line.enabled = true;

            Observable.Timer(TimeSpan.FromSeconds(_lazerDuration))
                .Subscribe(_ => line.enabled = false)
                .AddTo(this);
        }
    }
}
