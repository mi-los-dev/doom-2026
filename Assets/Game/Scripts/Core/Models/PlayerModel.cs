using System.Collections.Generic;
using UniRx;

namespace Game.Core
{
    public class PlayerModel
    {
        public ReactiveProperty<float> CurrentHp;
        public ReactiveProperty<int> UpgradePoints;
        public ReactiveProperty<float> MaxHp => _upgradableStats[StatIds.Health];
        public ReactiveProperty<float> MoveSpeed => _upgradableStats[StatIds.Speed];
        public ReactiveProperty<float> Damage => _upgradableStats[StatIds.Damage];

        private Dictionary<string, ReactiveProperty<float>> _upgradableStats;

        public void SetUpgradableStats(Dictionary<string, ReactiveProperty<float>> stats)
        {
            _upgradableStats = stats;
        }

        public bool TryGetUpgradableStat(string key, out ReactiveProperty<float> value)
        {
            if (_upgradableStats.TryGetValue(key, out var statProp))
            {
                value = statProp;
                return true;
            }

            value = null;
            return false;
        }
    }
}
