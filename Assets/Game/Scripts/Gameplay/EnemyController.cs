using Game.Core;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class EnemyMovement : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<EnemyMovement> { }

        [Inject] private readonly PlayerMovement _playerMovement;

        private void Update()
        {
            var direction = _playerMovement.transform.position - transform.position;
            direction.y = 0f;

            if (direction != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}