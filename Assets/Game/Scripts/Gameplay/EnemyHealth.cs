using DG.Tweening;
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

        public void TakeDamage(ShotInfotmation shotInfotmation)
        {
            Hp.Value -= shotInfotmation.Damage;
            if (Hp.Value <= 0)
            {
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