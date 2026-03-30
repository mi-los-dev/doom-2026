using System;
using Game.Core;
using Game.Services;
using Game.UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.UI.UpgradeWindow
{
    public class UpgradeWindowPresenter : IInitializable, IDisposable, IUpgradeWindow
    {
        [Inject] private readonly IUpgradeWindowView _view;
        [Inject] private readonly IWindowService _windowService;
        [Inject] private readonly UpgradeService _upgradeService;
        [Inject] private readonly StatsTableConfig _statsTableConfig;
        [Inject] private readonly IInstantiator _instantiator;
        [Inject] private readonly ILocalizationService _localizationService;
        [Inject] private readonly IInputProvider _inputProvider;

        private UpgradeSessionModel _session;
        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        private CompositeDisposable _sessionDisposables;

        public void Initialize()
        {
            _view.SetActive(false);

            _windowService.WindowOpened
                .Where(t => t == typeof(IUpgradeWindow))
                .Subscribe(_ => OpenView())
                .AddTo(_disposables);

            _view.OnApplyClicked.Subscribe(_ => Apply()).AddTo(_disposables);
            _view.OnCloseClicked.Subscribe(_ => Close()).AddTo(_disposables);

            _inputProvider.CloseUIInput()
                .Where(_ => _view.IsActive)
                .Subscribe(_ => Close())
                .AddTo(_disposables);
        }

        public void Dispose() => _disposables.Dispose();

        private void OpenView()
        {
            _sessionDisposables?.Dispose();
            _sessionDisposables = new CompositeDisposable();

            _session = _upgradeService.OpenSession();

            foreach (Transform child in _view.RowsContainer)
                UnityEngine.Object.Destroy(child.gameObject);

            foreach (var statDefinition in _statsTableConfig.StatDefinitions)
            {
                var row = _instantiator.InstantiatePrefabForComponent<UpgradeWindowStatView>(
                    _view.RowPrefab.gameObject,
                    _view.RowsContainer);

                row.Initialize(statDefinition, _session);
            }

            _session.RemainingPoints
                .Subscribe(points => _view.SetPointsText($"{_localizationService.Get("Points")}: {points}"))
                .AddTo(_sessionDisposables);

            _session.HasPendingChanges
                .Subscribe(hasChanges => _view.SetApplyButtonInteractable(hasChanges))
                .AddTo(_sessionDisposables);

            _view.SetApplyButtonInteractable(false);
            _view.SetActive(true);
        }

        private void Apply()
        {
            _upgradeService.Apply(_session);
            CloseView();
        }

        private void Close()
        {
            _upgradeService.Discard(_session);
            CloseView();
        }

        private void CloseView()
        {
            _sessionDisposables?.Dispose();
            _view.SetActive(false);
            _windowService.Close<IUpgradeWindow>();
        }
    }
}
