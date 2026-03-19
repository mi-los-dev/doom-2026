using System;
using System.Collections.Generic;
using UniRx;

namespace Game.Core
{
    [Serializable]
    public class PlayerSaveData
    {
        public Dictionary<string, int> StatLevels;
        public int UpgradePoints;
    }
}