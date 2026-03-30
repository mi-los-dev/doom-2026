using Game.Core;
using Game.Infrastructure;
using Zenject;

namespace Game.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IInputProvider>().To<PCInputProvider>().AsSingle();
            Container.Bind<ISaveService>().To<JsonSaveService>().AsSingle();
            Container.Bind<ILocalizationService>().To<StubLocalizationService>().AsSingle();
        }
    }
}
