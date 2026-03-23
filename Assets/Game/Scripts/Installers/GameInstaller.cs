using Game.Core;
using Game.Infrastructure;
using Game.Services;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private ScriptableObject[] _configs;

        public override void InstallBindings()
        {
            BindConfigs();
            BindServices();
        }

        private void BindConfigs()
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
        }

        private void BindServices()
        {
            Container.Bind<StatCalculationService>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpgradeService>().AsSingle();
            Container.Bind<EnemyRewardService>().AsSingle();
        }
    }
}
