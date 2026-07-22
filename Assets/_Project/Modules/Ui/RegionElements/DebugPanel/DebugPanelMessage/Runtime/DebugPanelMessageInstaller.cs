using JetBrains.Annotations;
using Zenject;

namespace GameKit.DebugPanelMessage
{
    [UsedImplicitly]
    public class DebugPanelMessageInstaller : Installer<DebugPanelMessageInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<DebugPanelMessagePresenter>().AsSingle();
        }
    }
}
