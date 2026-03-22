using System;
using Game.Core;
using Game.Services;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.UI.UpgradeWindow
{
    public class UpgradeWindowPresenter : IInitializable
    {
        public IObservable<bool> IsOpen => _isOpen;

        [Inject] private readonly UpgradeWindowView _view;
        [Inject] private readonly UpgradeService _upgradeService;
        [Inject] private readonly StatsTableConfig _statsTableConfig;
        [Inject] private readonly IInstantiator _instantiator;
        [Inject] private readonly ILocalizationService _localizationService;
        [Inject] private readonly IInputProvider _inputProvider;

        private UpgradeSessionModel _session;
        private readonly Subject<bool> _isOpen = new Subject<bool>();

        public void Initialize()
        {
            _view.gameObject.SetActive(false);

            _view.ApplyButton
                .OnClickAsObservable()
                .Subscribe(_ => Apply())
                .AddTo(_view);

            _view.CloseButton
                .OnClickAsObservable()
                .Subscribe(_ => Close())
                .AddTo(_view);

            _inputProvider.CloseUIInput()
                .Where(_ => _view.gameObject.activeSelf)
                .Subscribe(_ => Close())
                .AddTo(_view);
        }

        public void Open()
        {
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
                .Subscribe(points => _view.PointsText.text = $"{_localizationService.Get("Points")}: {points}")
                .AddTo(_view);

            _session.HasPendingChanges
                .Subscribe(hasChanges => _view.ApplyButton.interactable = hasChanges)
                .AddTo(_view);

            _view.ApplyButton.interactable = false;
            _view.gameObject.SetActive(true);
            _isOpen.OnNext(true);
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
            _view.gameObject.SetActive(false);
            _isOpen.OnNext(false);
        }
    }
}
