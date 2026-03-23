using System.Collections.Generic;
using Game.Core;
using Game.Gameplay;
using Game.Infrastructure;
using Game.Services;
using Game.UI.Hud;
using Game.UI.UpgradeWindow;
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
            BindServices();
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

        private void BindServices()
        {
            Container.Bind<StatCalculationService>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpgradeService>().AsSingle();
            Container.Bind<EnemyRewardService>().AsSingle();
        }

        private void BindUI()
        {
            Container.Bind<IHudView>().To<HudView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IUpgradeWindowView>().To<UpgradeWindowView>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<HudPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpgradeWindowPresenter>().AsSingle();
        }
    }
}
