using JetBrains.Annotations;
using Zenject;

namespace GameKit.DebugToolBar
{
    [UsedImplicitly]
    public class DebugToolBarInstaller : Installer<DebugToolBarInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<DebugToolBarPageTabsPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<DebugToolBarLogsIndicatorPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<DebugToolBarCloseButtonPresenter>().AsSingle();
        }
    }
}
