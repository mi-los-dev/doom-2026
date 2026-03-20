using Game.Core;
using Game.Services;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class EnemyHealth : MonoBehaviour
    {
        public ReactiveProperty<float> Hp;

        [SerializeField] private ParticleSystem _takeDamageEffect;

        [Inject] EnemyRewardService _enemyRewardService;
        [Inject] EnemyConfig _enemyConfig;

        private void Awake()
        {
            Hp.Value = Random.Range(_enemyConfig.MinHp, _enemyConfig.MaxHp + 1);
        }

        public void TakeDamage(float amount, RaycastHit hit)
        {
            Hp.Value -= amount;
            if (Hp.Value <= 0)
            {
                _enemyRewardService.OnEnemyKilled();
                Destroy(gameObject);
            }
            else
            {
                _takeDamageEffect.transform.position = hit.point;
                _takeDamageEffect.transform.LookAt(hit.point + hit.normal);
                _takeDamageEffect.Stop();
                _takeDamageEffect.Play();
            }
        }
    }
}