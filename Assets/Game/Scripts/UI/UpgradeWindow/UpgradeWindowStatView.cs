using Game.Core;
using Game.Services;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.UpgradeWindow
{
    public class UpgradeWindowStatView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _statNameText;
        [SerializeField] private Image _line;
        [SerializeField] private Button _plusButton;

        [Inject] private readonly ILocalizationService _localizationService;
        [Inject] private readonly StatCalculationService _statCalculationService;

        private StatDefinition _statDefinition;

        public void Initialize(StatDefinition statDefinition, UpgradeSessionModel session)
        {
            _statDefinition = statDefinition;

            session.GetDisplayLevel(_statDefinition.Id)
                .Subscribe(RefreshView)
                .AddTo(this);

            session.GetDisplayLevel(_statDefinition.Id)
                .CombineLatest(session.RemainingPoints,
                    (level, points) => points > 0 && level < _statDefinition.MaxUpgradeLevel)
                .Subscribe(canUpgrade => _plusButton.interactable = canUpgrade)
                .AddTo(this);

            _plusButton.OnClickAsObservable()
                .Subscribe(_ => session.Allocate(_statDefinition.Id))
                .AddTo(this);
        }

        private void RefreshView(int level)
        {
            var statName = _localizationService.Get(_statDefinition.LocalizationKey);
            var value = _statCalculationService.CalculateValue(_statDefinition, level);

            _statNameText.text = $"{statName}: {value:0.##}";
            _line.fillAmount = (float)level / _statDefinition.MaxUpgradeLevel;
        }
    }
}
