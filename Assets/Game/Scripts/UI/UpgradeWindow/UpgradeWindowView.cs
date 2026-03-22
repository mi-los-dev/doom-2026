using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.UpgradeWindow
{
    public class UpgradeWindowView : MonoBehaviour
    {
        public TMP_Text PointsText => _pointsText;
        public Transform RowsContainer => _rowsContainer;
        public Button ApplyButton => _applyButton;
        public Button CloseButton => _closeButton;
        public UpgradeWindowStatView RowPrefab => _rowPrefab;

        [SerializeField] private TMP_Text _pointsText;
        [SerializeField] private Transform _rowsContainer;
        [SerializeField] private Button _applyButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private UpgradeWindowStatView _rowPrefab;
    }
}
