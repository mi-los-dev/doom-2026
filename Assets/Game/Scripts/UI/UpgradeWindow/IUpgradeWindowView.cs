using System;
using UniRx;
using UnityEngine;

namespace Game.UI.UpgradeWindow
{
    public interface IUpgradeWindowView
    {
        IObservable<Unit> OnApplyClicked { get; }
        IObservable<Unit> OnCloseClicked { get; }
        Transform RowsContainer { get; }
        UpgradeWindowStatView RowPrefab { get; }
        bool IsActive { get; }
        void SetApplyButtonInteractable(bool interactable);
        void SetPointsText(string text);
        void SetActive(bool active);
    }
}
