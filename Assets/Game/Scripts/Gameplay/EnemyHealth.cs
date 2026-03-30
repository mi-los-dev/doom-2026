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
        [SerializeField] private ParticleSystem _destroyEffect;

        [Inject] IEnemyRewardService _enemyRewardService;
        [Inject] EnemyConfig _enemyConfig;

        private void Awake()
        {
            Hp.Value = Random.Range(_enemyConfig.MinHp, _enemyConfig.MaxHp + 1);
        }

        public void TakeDamage(ShotInfotmation shotInfotmation)
        {
            Hp.Value -= shotInfotmation.Damage;
            if (Hp.Value <= 0)
            {
                _destroyEffect.Play();
                _destroyEffect.transform.SetParent(null);
                _destroyEffect.transform.position = shotInfotmation.HitPoint;
                Destroy(_destroyEffect.gameObject, 2f);
                _enemyRewardService.OnEnemyKilled();
                Destroy(gameObject);
            }
            else
            {
                _takeDamageEffect.transform.SetPositionAndRotation(
                    shotInfotmation.HitPoint,
                    Quaternion.LookRotation(-shotInfotmation.HitDirection));
                _takeDamageEffect.Play();
            }
        }
    }
}