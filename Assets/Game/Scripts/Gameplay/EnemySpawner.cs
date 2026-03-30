using Game.Core;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float _minZPos;
        [SerializeField] private float _maxZPos;
        [SerializeField] private float _minXPos;
        [SerializeField] private float _maxXPos;
        [SerializeField] private int _initialEnemyCount = 5;

        [Inject] private EnemyMovement.Factory _enemyFactory;
        [Inject] private readonly IEnemyRewardService _enemyRewardService;

        private void Start()
        {
            for (int i = 0; i < _initialEnemyCount; i++)
                SpawnEnemy(GetRandomSpawnPoint());

            _enemyRewardService.EnemyKilled
                .Subscribe(_ => SpawnEnemy(GetRandomSpawnPoint()))
                .AddTo(this);
        }

        private EnemyMovement SpawnEnemy(Vector3 position)
        {
            var enemy = _enemyFactory.Create();
            enemy.transform.position = position;
            return enemy;
        }

        private Vector3 GetRandomSpawnPoint()
        {
            return new Vector3((int)Random.Range(_minXPos, _maxXPos), 0, (int)Random.Range(_minZPos, _maxZPos));
        }
    }
}
