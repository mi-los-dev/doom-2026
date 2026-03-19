using Game.Gameplay;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _enemyPrefab;

        public override void InstallBindings()
        {
            Container.BindFactory<EnemyController, EnemyController.Factory>()
                .FromComponentInNewPrefab(_enemyPrefab).AsSingle();
        }
    }
}
