using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.UpgradeWindow
{
    public class UpgradeWindowView : MonoBehaviour, IUpgradeWindowView
    {
        [SerializeField] private TMP_Text _pointsText;
        [SerializeField] private Transform _rowsContainer;
        [SerializeField] private Button _applyButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private UpgradeWindowStatView _rowPrefab;

        public IObservable<Unit> OnApplyClicked => _applyButton.OnClickAsObservable();
        public IObservable<Unit> OnCloseClicked => _closeButton.OnClickAsObservable();
        public Transform RowsContainer => _rowsContainer;
        public UpgradeWindowStatView RowPrefab => _rowPrefab;
        public bool IsActive => gameObject.activeSelf;

        public void SetApplyButtonInteractable(bool interactable) => _applyButton.interactable = interactable;
        public void SetPointsText(string text) => _pointsText.text = text;
        public void SetActive(bool active) => gameObject.SetActive(active);
    }
}
