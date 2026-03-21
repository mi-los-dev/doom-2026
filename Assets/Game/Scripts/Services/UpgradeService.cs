using System.Collections.Generic;
using System.Linq;
using Game.Core;
using UnityEngine;
using Zenject;

namespace Game.Services
{
    public class UpgradeService : IInitializable
    {
        [Inject] private readonly PlayerModel _playerModel;
        [Inject] private readonly StatsTableConfig _statsTableConfig;
        [Inject] private readonly ISaveService _saveService;
        [Inject] private readonly StatCalculationService _statCalculationService;

        public void Initialize()
        {
            var saveData = _saveService.Load();
            if (saveData == null) return;

            _playerModel.UpgradePoints.Value = saveData.UpgradePoints;

            foreach (var def in _statsTableConfig.StatDefinitions)
            {
                if (!saveData.StatLevels.TryGetValue(def.Id, out var level) || level == 0) continue;

                var value = _statCalculationService.CalculateValue(def, level);
                if (_playerModel.TryGetUpgradableStat(def.Id, out var prop))
                    prop.Value = value;
            }

            _playerModel.CurrentHp.Value = _playerModel.MaxHp.Value;
        }

        public UpgradeSessionModel OpenSession()
        {
            var saveData = _saveService.Load();
            var currentLevels = saveData?.StatLevels ?? new Dictionary<string, int>();
            return new UpgradeSessionModel(_playerModel.UpgradePoints.Value, currentLevels, _statsTableConfig);
        }

        public void Apply(UpgradeSessionModel session)
        {
            var saveData = _saveService.Load() ?? new PlayerSaveData { StatLevels = new Dictionary<string, int>() };

            foreach (var kvp in session.PendingAllocations)
            {
                if (kvp.Value == 0) continue;

                saveData.StatLevels.TryGetValue(kvp.Key, out var currentLevel);
                var newLevel = currentLevel + kvp.Value;
                saveData.StatLevels[kvp.Key] = newLevel;

                var statDefinition = _statsTableConfig.StatDefinitions.First(d => d.Id == kvp.Key);
                if (statDefinition == null) continue;

                var newValue = _statCalculationService.CalculateValue(statDefinition, newLevel);

                if (_playerModel.TryGetUpgradableStat(kvp.Key, out var statProp))
                    statProp.Value = newValue;

                if (kvp.Key == StatIds.Health)
                    _playerModel.CurrentHp.Value = Mathf.Min(_playerModel.CurrentHp.Value, newValue) + (newValue - _playerModel.MaxHp.Value);
            }

            _playerModel.UpgradePoints.Value -= session.SpentPoints;
            saveData.UpgradePoints = _playerModel.UpgradePoints.Value;

            _saveService.Save(saveData);
        }

        public void Discard(UpgradeSessionModel session)
        {
            session.Reset();
        }
    }
}
