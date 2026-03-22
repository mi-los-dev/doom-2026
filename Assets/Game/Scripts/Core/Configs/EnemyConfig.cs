using UnityEngine;

namespace Game.Core
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig", order = 1)]
    public class EnemyConfig : ScriptableObject
    {
        public virtual int MinHp => _minHp;
        public virtual int MaxHp => _maxHp;

        [SerializeField] protected int _minHp;
        [SerializeField] protected int _maxHp;
    }
}