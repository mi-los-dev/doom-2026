namespace Game.Core
{
    public interface IStatCalculationService
    {
        float CalculateValue(StatDefinition stat, int level);
    }
}
