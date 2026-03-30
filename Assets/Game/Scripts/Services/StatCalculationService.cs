using Game.Core;

namespace Game.Services
{
    public class StatCalculationService : IStatCalculationService
    {
        public float CalculateValue(StatDefinition stat, int level)
        {
            return stat.StartValue * (1f + stat.ValuePerPoint * level);
        }
    }
}