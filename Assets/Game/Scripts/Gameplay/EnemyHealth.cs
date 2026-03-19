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

        [Inject] EnemyRewardService _enemyRewardService;
        [Inject] EnemyConfig _enemyConfig;

        public void TakeDamage(float amount)
        {
            Hp.Value -= amount;
            if (Hp.Value <= 0)
            {
                _enemyRewardService.OnEnemyKilled();
                Destroy(gameObject);
            }
        }
    }
}