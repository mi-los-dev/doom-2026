using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Hud
{
    public class HudView : MonoBehaviour, IHudView
    {
        [SerializeField] private TMP_Text _pointsText;
        [SerializeField] private TMP_Text _hpText;
        [SerializeField] private Image _hpLine;
        [SerializeField] private Transform _statsContainer;
        [SerializeField] private HudStatView _statViewPrefab;
        [SerializeField] private Button _upgradeButton;

        public IObservable<Unit> OnUpgradeClicked => _upgradeButton.OnClickAsObservable();
        public Transform StatsContainer => _statsContainer;
        public HudStatView StatViewPrefab => _statViewPrefab;

        public void SetHp(float current, float max)
        {
            _hpText.text = $"{current:0.##} / {max:0.##}";
            _hpLine.fillAmount = current / max;
        }

        public void SetPointsText(string text) => _pointsText.text = text;
        public void SetActive(bool active) => gameObject.SetActive(active);
    }
}
