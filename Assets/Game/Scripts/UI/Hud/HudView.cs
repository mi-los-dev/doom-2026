using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Hud
{
    public class HudView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _pointsText;
        [SerializeField] private TMP_Text _hpText;
        [SerializeField] private Image _hpLine;
        [SerializeField] private Transform _statsContainer;
        [SerializeField] private HudStatView _statViewPrefab;
        [SerializeField] private Button _upgradeButton;

        public TMP_Text PointsText => _pointsText;
        public TMP_Text HpText => _hpText;
        public Image HpLine => _hpLine;
        public Transform StatsContainer => _statsContainer;
        public HudStatView StatViewPrefab => _statViewPrefab;
        public Button UpgradeButton => _upgradeButton;
    }
}
