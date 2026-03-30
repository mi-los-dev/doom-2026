using Game.Core;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class EnemyMovement : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<EnemyMovement> { }

        [Inject] private readonly IPlayerPositionProvider _playerPositionProvider;

        private void Update()
        {
            var direction = _playerPositionProvider.Position - transform.position;
            direction.y = 0f;

            if (direction != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
