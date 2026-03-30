using Game.Infrastructure;
using Zenject;

namespace Game.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameBootstrapper>().AsSingle();
        }
    }
}
