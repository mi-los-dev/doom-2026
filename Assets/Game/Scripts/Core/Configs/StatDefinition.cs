using UnityEngine;

namespace Game.Core
{
    [CreateAssetMenu(fileName = "StatDefinition", menuName = "Configs/StatDefinition", order = 1)]
    public class StatDefinition : ScriptableObject
    {
        public virtual string Id => _id;
        public virtual string LocalizationKey => _localizationKey;
        public virtual float StartValue => _startValue;
        public virtual float ValuePerPoint => _valuePerPoint;
        public virtual int MaxValue => _maxValue;

        [SerializeField] protected string _id;
        [SerializeField] protected string _localizationKey;
        [SerializeField] protected float _startValue;
        [SerializeField] protected float _valuePerPoint;
        [SerializeField] protected int _maxValue;
    }
}