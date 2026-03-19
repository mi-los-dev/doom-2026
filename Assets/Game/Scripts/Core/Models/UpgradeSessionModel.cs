using System.Collections.Generic;
using UniRx;

namespace Game.Core
{
    public class UpgradeSessionModel
    {
        public Dictionary<string, int> PendingAllocations;
        public ReactiveProperty<int> RemainingPoints;

        public void Allocate(string statId)
        {

        }

        public void Reset()
        {

        }
    }
}