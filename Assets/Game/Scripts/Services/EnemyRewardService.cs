using Game.Core;
using Zenject;

namespace Game.Services
{
    public class EnemyRewardService
    {
        [Inject] private PlayerModel _playerModel;

        public void OnEnemyKilled()
        {
            _playerModel.UpgradePoints.Value++;
        }
    }
}