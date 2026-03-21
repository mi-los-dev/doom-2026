using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Game.Core
{
    public class UpgradeSessionModel
    {
        public IReadOnlyReactiveProperty<int> RemainingPoints => _remainingPoints;

        public IObservable<bool> HasPendingChanges =>
            _remainingPoints.Select(points => points < _initialPoints);

        private readonly ReactiveProperty<int> _remainingPoints;
        private readonly Dictionary<string, int> _pendingAllocations = new Dictionary<string, int>();
        private readonly Dictionary<string, ReactiveProperty<int>> _displayLevels = new Dictionary<string, ReactiveProperty<int>>();
        private readonly Dictionary<string, int> _currentLevels;
        private readonly StatsTableConfig _statsConfig;
        private readonly int _initialPoints;

        public UpgradeSessionModel(int availablePoints, Dictionary<string, int> currentLevels, StatsTableConfig statsConfig)
        {
            _initialPoints = availablePoints;
            _remainingPoints = new ReactiveProperty<int>(availablePoints);
            _currentLevels = currentLevels;
            _statsConfig = statsConfig;

            foreach (var def in statsConfig.StatDefinitions)
            {
                var level = currentLevels.TryGetValue(def.Id, out var l) ? l : 0;
                _displayLevels[def.Id] = new ReactiveProperty<int>(level);
            }
        }

        public int SpentPoints => _initialPoints - _remainingPoints.Value;

        public IReadOnlyDictionary<string, int> PendingAllocations => _pendingAllocations;

        public IReadOnlyReactiveProperty<int> GetDisplayLevel(string statId) => _displayLevels[statId];

        public void Allocate(string statId)
        {
            if (_remainingPoints.Value <= 0) return;

            var statDefinition = _statsConfig.StatDefinitions.First(d => d.Id == statId);
            if (statDefinition == null) return;

            var currentLevel = (_currentLevels.TryGetValue(statId, out var cl) ? cl : 0)
                             + (_pendingAllocations.TryGetValue(statId, out var pa) ? pa : 0);

            if (currentLevel >= statDefinition.MaxUpgradeLevel) return;

            _pendingAllocations[statId] = (_pendingAllocations.TryGetValue(statId, out var p) ? p : 0) + 1;
            _displayLevels[statId].Value++;
            _remainingPoints.Value--;
        }

        public void Reset()
        {
            _pendingAllocations.Clear();
            _remainingPoints.Value = _initialPoints;

            foreach (var def in _statsConfig.StatDefinitions)
            {
                var level = _currentLevels.TryGetValue(def.Id, out var l) ? l : 0;
                _displayLevels[def.Id].Value = level;
            }
        }
    }
}
