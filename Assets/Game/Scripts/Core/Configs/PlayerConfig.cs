using UnityEngine;

namespace Game.Core
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig", order = 1)]
    public class PlayerConfig : ScriptableObject
    {
        public virtual float BaseSpeed => _baseSpeed;
        public virtual float BaseHp => _baseHp;
        public virtual float BaseDamage => _baseDamage;
        public virtual int StartUpgradePoints => _startUpgradePoints;

        [SerializeField] private float _baseSpeed;
        [SerializeField] private float _baseHp;
        [SerializeField] private float _baseDamage;
        [SerializeField] private int _startUpgradePoints;
    }
}