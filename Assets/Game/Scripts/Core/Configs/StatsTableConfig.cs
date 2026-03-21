using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    [CreateAssetMenu(fileName = "StatsTableConfig", menuName = "Configs/StatsTableConfig", order = 1)]
    public class StatsTableConfig : ScriptableObject
    {
        public virtual List<StatDefinition> StatDefinitions => _statDefinitions;

        [SerializeField] protected List<StatDefinition> _statDefinitions;
    }
}