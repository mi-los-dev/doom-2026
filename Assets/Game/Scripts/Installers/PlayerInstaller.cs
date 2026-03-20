using Game.Gameplay;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerMovement>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerHealth>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerShooter>().FromComponentInHierarchy().AsSingle();
    }
}
