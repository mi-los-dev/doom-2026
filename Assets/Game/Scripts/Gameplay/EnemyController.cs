using Game.Core;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class EnemyMovement : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<EnemyMovement> { }
    }
}