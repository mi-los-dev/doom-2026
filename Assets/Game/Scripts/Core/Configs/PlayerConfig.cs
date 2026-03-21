using UnityEngine;

namespace Game.Core
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig", order = 1)]
    public class PlayerConfig : ScriptableObject
    {
        public virtual int StartUpgradePoints => _startUpgradePoints;

        [SerializeField] private int _startUpgradePoints;
    }
}