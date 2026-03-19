using Game.Core;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class EnemySpawner : MonoBehaviour
    {
        [Inject] private EnemyController.Factory _enemyFactory;
        [Inject] private EnemyConfig _enemyConfig;

        public EnemyController SpawnEnemy(Vector3 position)
        {
            EnemyController enemy = _enemyFactory.Create();
            enemy.transform.position = position;
            return enemy;
        }
    }
}