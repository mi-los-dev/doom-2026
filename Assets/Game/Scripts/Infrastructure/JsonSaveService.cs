using System;
using System.Collections.Generic;
using Game.Core;
using UnityEngine;

namespace Game.Infrastructure
{
    public class JsonSaveService : ISaveService
    {
        private const string SaveKey = "player_save";

        [Serializable]
        private class RawSaveData
        {
            public string[] StatIds;
            public int[] StatLevels;
            public int UpgradePoints;
            public float CurrentHp;
        }

        public PlayerSaveData Load()
        {
            if (!PlayerPrefs.HasKey(SaveKey))
                return null;

            var raw = JsonUtility.FromJson<RawSaveData>(PlayerPrefs.GetString(SaveKey));
            if (raw == null)
                return null;

            var statLevels = new Dictionary<string, int>();
            if (raw.StatIds != null)
            {
                for (int i = 0; i < raw.StatIds.Length; i++)
                    statLevels[raw.StatIds[i]] = raw.StatLevels[i];
            }

            return new PlayerSaveData
            {
                StatLevels = statLevels,
                UpgradePoints = raw.UpgradePoints,
                CurrentHp = raw.CurrentHp
            };
        }

        public void Save(PlayerSaveData data)
        {
            var ids = new string[data.StatLevels.Count];
            var levels = new int[data.StatLevels.Count];
            int i = 0;
            foreach (var kvp in data.StatLevels)
            {
                ids[i] = kvp.Key;
                levels[i] = kvp.Value;
                i++;
            }

            var raw = new RawSaveData
            {
                StatIds = ids,
                StatLevels = levels,
                UpgradePoints = data.UpgradePoints,
                CurrentHp = data.CurrentHp
            };

            PlayerPrefs.SetString(SaveKey, JsonUtility.ToJson(raw));
            PlayerPrefs.Save();
        }
    }
}
