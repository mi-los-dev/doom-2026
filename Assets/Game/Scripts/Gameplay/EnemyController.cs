using Game.Core;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class EnemyController : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<EnemyController> { }

        [Inject] private EnemyConfig _enemyConfig;
    }
}