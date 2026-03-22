using System.Collections.Generic;
using Game.Core;
using Game.Gameplay;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindModels();
            BindPlayer();
        }

        private void BindModels()
        {
            Container.Bind<PlayerModel>().FromMethod(CreatePlayerModel).AsSingle();
        }

        private void BindPlayer()
        {
            Container.Bind<PlayerMovement>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerHealth>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerShooter>().FromComponentInHierarchy().AsSingle();
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
