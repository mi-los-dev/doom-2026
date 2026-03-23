using System;
using Game.Core;
using Game.UI.UpgradeWindow;
using UniRx;
using Zenject;

namespace Game.UI.Hud
{
    public class HudPresenter : IInitializable, IDisposable
    {
        [Inject] private readonly IHudView _view;
        [Inject] private readonly PlayerModel _playerModel;
        [Inject] private readonly StatsTableConfig _statsTableConfig;
        [Inject] private readonly ILocalizationService _localizationService;
        [Inject] private readonly UpgradeWindowPresenter _upgradeWindowPresenter;
        [Inject] private readonly IInstantiator _instantiator;
        [Inject] private readonly IInputProvider _inputProvider;

        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public void Initialize()
        {
            BindHp();
            BindPoints();
            BindStats();
            BindUpgradeButton();
            BindWindowVisibility();
        }

        public void Dispose() => _disposables.Dispose();

        private void BindHp()
        {
            Observable.CombineLatest(_playerModel.CurrentHp, _playerModel.MaxHp, (cur, max) => (cur, max))
                .Subscribe(v => _view.SetHp(v.cur, v.max))
                .AddTo(_disposables);
        }

        private void BindPoints()
        {
            _playerModel.UpgradePoints
                .Subscribe(points => _view.SetPointsText($"{_localizationService.Get("Points")}: {points}"))
                .AddTo(_disposables);
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
                statProp.Subscribe(val => statView.SetText($"{statName}: {val:0.##}")).AddTo(_disposables);
            }
        }

        private void BindUpgradeButton()
        {
            _view.OnUpgradeClicked
                .Subscribe(_ => _upgradeWindowPresenter.Open())
                .AddTo(_disposables);

            _inputProvider.UpgradeUIInput()
                .Subscribe(_ => _upgradeWindowPresenter.Open())
                .AddTo(_disposables);
        }

        private void BindWindowVisibility()
        {
            _upgradeWindowPresenter.IsOpen
                .Subscribe(isOpen => _view.SetActive(!isOpen))
                .AddTo(_disposables);
        }
    }
}
