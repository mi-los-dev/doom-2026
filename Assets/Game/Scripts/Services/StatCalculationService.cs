using Game.Core;

namespace Game.Services
{
    public class StatCalculationService
    {
        public float CalculateValue(StatDefinition stat, int level)
        {
            return stat.StartValue + (stat.ValuePerPoint * level);
        }
    }
}