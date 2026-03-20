using Game.Core;
using Game.Infrastructure;
using Game.Services;
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
            Container.Bind<UpgradeService>().AsSingle();
            Container.Bind<EnemyRewardService>().AsSingle();
        }

        private PlayerModel CreatePlayerModel(InjectContext ctx)
        {
            var config = ctx.Container.Resolve<IConfigProvider>().Get<PlayerConfig>();

            return new PlayerModel
            {
                CurrentHp = new ReactiveProperty<float>(config.BaseHp),
                MaxHp = new ReactiveProperty<float>(config.BaseHp),
                MoveSpeed = new ReactiveProperty<float>(config.BaseSpeed),
                Damage = new ReactiveProperty<float>(config.BaseDamage),
                UpgradePoints = new ReactiveProperty<int>(config.StartUpgradePoints)
            };
        }
    }
}