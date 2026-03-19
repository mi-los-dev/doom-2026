using UnityEngine;

namespace Game.Core
{
    [CreateAssetMenu(fileName = "StatsTableConfig", menuName = "Configs/StatsTableConfig", order = 1)]
    public class StatsTableConfig : ScriptableObject
    {
        public virtual StatDefinition[] StatDefinitions => _statDefinitions;

        [SerializeField] protected StatDefinition[] _statDefinitions;
    }
}