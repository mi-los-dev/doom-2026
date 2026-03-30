using System;
using UniRx;

namespace Game.Core
{
    public interface IEnemyRewardService
    {
        IObservable<Unit> EnemyKilled { get; }
        void OnEnemyKilled();
    }
}
