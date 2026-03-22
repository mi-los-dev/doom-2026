using Game.Core;
using Game.UI.UpgradeWindow;
using UniRx;
using Zenject;

namespace Game.UI.Hud
{
    public class HudPresenter : IInitializable
    {
        [Inject] private readonly HudView _view;
        [Inject] private readonly PlayerModel _playerModel;
        [Inject] private readonly StatsTableConfig _statsTableConfig;
        [Inject] private readonly ILocalizationService _localizationService;
        [Inject] private readonly UpgradeWindowPresenter _upgradeWindowPresenter;
        [Inject] private readonly IInstantiator _instantiator;
        [Inject] private readonly IInputProvider _inputProvider;

        public void Initialize()
        {
            BindHp();
            BindPoints();
            BindStats();
            BindUpgradeButton();
            BindWindowVisibility();
        }

        private void BindHp()
        {
            _playerModel.CurrentHp
                .Subscribe(hp =>
                {
                    _view.HpText.text = $"{(int)hp} / {(int)_playerModel.MaxHp.Value}";
                    _view.HpLine.fillAmount = hp / _playerModel.MaxHp.Value;
                })
                .AddTo(_view);
        }

        private void BindPoints()
        {
            _playerModel.UpgradePoints
               .Subscribe(points => _view.PointsText.text
                   = $"{_localizationService.Get("Points")}: {points}")
               .AddTo(_view);
        }

        private void BindStats()
        {
            foreach (var statDefinition in _statsTableConfig.StatDefinitions)
            {
                if (!_playerModel.TryGetUpgradableStat(statDefinition.Id, out var statProp)) continue;

                var statView = _instantiator.InstantiatePrefabForComponent<HudStatView>(
                    _view.StatViewPrefab,
                    _view.StatsContainer);

                var statName = _localizationService.Get(statDefinition.LocalizationKey);

                statProp.Subscribe(val => statView.SetText($"{statName}: {val:0.##}")).AddTo(_view);
            }
        }

        private void BindUpgradeButton()
        {
            _view.UpgradeButton
                .OnClickAsObservable()
                .Subscribe(_ => _upgradeWindowPresenter.Open())
                .AddTo(_view);

            _inputProvider.UpgradeUIInput()
                .Subscribe(_ => _upgradeWindowPresenter.Open())
                .AddTo(_view);
        }

        private void BindWindowVisibility()
        {
            _upgradeWindowPresenter.IsOpen
                .Subscribe(isOpen => _view.gameObject.SetActive(!isOpen))
                .AddTo(_view);
        }
    }
}
