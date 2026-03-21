using System.Collections.Generic;
using System.Linq;
using Game.Core;
using Game.Gameplay;
using Game.Infrastructure;
using Game.Services;
using Game.UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private ScriptableObject[] _configs;

        public override void InstallBindings()
        {
            BindInfrastructure();
            BindModels();
            BindServices();
            BindPlayer();
            BindUI();
        }

        private void BindInfrastructure()
        {
            Container.Bind<IConfigProvider>()
                .FromMethod(_ => new LocalSOConfigProvider(_configs))
                .AsSingle();

            Container.Bind<PlayerConfig>()
                .FromMethod(ctx => ctx.Container.Resolve<IConfigProvider>().Get<PlayerConfig>())
                .AsSingle();
            Container.Bind<EnemyConfig>()
                .FromMethod(ctx => ctx.Container.Resolve<IConfigProvider>().Get<EnemyConfig>())
                .AsSingle();
            Container.Bind<StatsTableConfig>()
                .FromMethod(ctx => ctx.Container.Resolve<IConfigProvider>().Get<StatsTableConfig>())
                .AsSingle();

            Container.Bind<IInputProvider>().To<PCInputProvider>().AsSingle();
            Container.Bind<ISaveService>().To<JsonSaveService>().AsSingle();
            Container.Bind<ILocalizationService>().To<StubLocalizationService>().AsSingle();
        }

        private void BindModels()
        {
            Container.Bind<PlayerModel>().FromMethod(CreatePlayerModel).AsSingle();
        }

        private void BindServices()
        {
            Container.Bind<StatCalculationService>().AsSingle();

            Container.BindInterfacesAndSelfTo<UpgradeService>().AsSingle();

            Container.Bind<EnemyRewardService>().AsSingle();
        }

        private void BindPlayer()
        {
            Container.Bind<PlayerMovement>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerHealth>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerShooter>().FromComponentInHierarchy().AsSingle();
        }

        private void BindUI()
        {
            Container.Bind<HudView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UpgradeWindowView>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<HudPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpgradeWindowPresenter>().AsSingle();
        }

        private PlayerModel CreatePlayerModel(InjectContext ctx)
        {
            var config = ctx.Container.Resolve<IConfigProvider>().Get<PlayerConfig>();
            var statsTable = ctx.Container.Resolve<IConfigProvider>().Get<StatsTableConfig>();

            var upgradableStats = new Dictionary<string, ReactiveProperty<float>>();
            foreach (var def in statsTable.StatDefinitions)
                upgradableStats[def.Id] = new ReactiveProperty<float>(def.StartValue);

            upgradableStats.TryGetValue(StatIds.Health, out var healthProp);
            var initialHp = healthProp?.Value ?? 0f;

            var playerModel = new PlayerModel
            {
                CurrentHp = new ReactiveProperty<float>(initialHp),
                UpgradePoints = new ReactiveProperty<int>(config.StartUpgradePoints)
            };
            playerModel.SetUpgradableStats(upgradableStats);

            return playerModel;
        }
    }
}
