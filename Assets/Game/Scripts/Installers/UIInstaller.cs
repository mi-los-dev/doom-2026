using Game.UI.Hud;
using Game.UI.UpgradeWindow;
using Zenject;

namespace Game.Installers
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IHudView>().To<HudView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IUpgradeWindowView>().To<UpgradeWindowView>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<HudPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpgradeWindowPresenter>().AsSingle();
        }
    }
}
