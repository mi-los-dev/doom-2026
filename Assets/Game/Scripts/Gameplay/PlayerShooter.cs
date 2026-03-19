using Game.Core;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class PlayerShooter : MonoBehaviour
    {
        [Inject] IInputProvider _inputProvider;
        [Inject] PlayerModel _playerModel;
    }
}