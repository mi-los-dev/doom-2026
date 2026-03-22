using Game.Core;
using System;
using UniRx;
using Zenject;

namespace Game.Services
{
    public class EnemyRewardService
    {
        public IObservable<Unit> EnemyKilled => _enemyKilled;

        [Inject] private PlayerModel _playerModel;

        private readonly Subject<Unit> _enemyKilled = new Subject<Unit>();

        public void OnEnemyKilled()
        {
            _playerModel.UpgradePoints.Value++;
            _enemyKilled.OnNext(Unit.Default);
        }
    }
}