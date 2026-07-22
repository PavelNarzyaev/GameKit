using JetBrains.Annotations;
using Zenject;

namespace GameKit.UiDebugPanel
{
    [UsedImplicitly]
    public class UiDebugPanelInstaller : Installer<UiDebugPanelInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<DebugPanelPageNavigator>().AsSingle();
            Container.BindInterfacesAndSelfTo<DebugPanelMessageNavigator>().AsSingle();
        }
    }
}
