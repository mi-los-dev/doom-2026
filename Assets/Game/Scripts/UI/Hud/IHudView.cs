using System;
using UniRx;
using UnityEngine;

namespace Game.UI.Hud
{
    public interface IHudView
    {
        IObservable<Unit> OnUpgradeClicked { get; }
        Transform StatsContainer { get; }
        HudStatView StatViewPrefab { get; }
        void SetHp(float current, float max);
        void SetPointsText(string text);
        void SetActive(bool active);
    }
}
