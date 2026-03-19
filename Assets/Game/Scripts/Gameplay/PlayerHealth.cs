using Game.Core;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class PlayerHealth : MonoBehaviour
    {
        [Inject] PlayerModel _playerModel;

        public void TakeDamage(float amount)
        {
            _playerModel.CurrentHp.Value -= amount;
        }
    }
}