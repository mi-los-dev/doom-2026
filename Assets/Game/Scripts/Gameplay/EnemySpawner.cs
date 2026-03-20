using Game.Core;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class EnemySpawner : MonoBehaviour
    {
        [Inject] private EnemyMovement.Factory _enemyFactory;

        public EnemyMovement SpawnEnemy(Vector3 position)
        {
            var enemy = _enemyFactory.Create();
            enemy.transform.position = position;
            return enemy;
        }
    }
}