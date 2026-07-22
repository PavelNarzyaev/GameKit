using JetBrains.Annotations;
using Zenject;

namespace GameKit.LogsDebugPage
{
    [UsedImplicitly]
    public class LogsDebugPageInstaller : Installer<LogsDebugPageInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LogsDebugPagePresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<DebugLogsFilterSelectorPresenter>().AsSingle();
        }
    }
}
