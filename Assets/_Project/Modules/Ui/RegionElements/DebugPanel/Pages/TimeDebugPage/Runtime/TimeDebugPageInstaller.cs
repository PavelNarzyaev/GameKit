using JetBrains.Annotations;
using Zenject;

namespace GameKit.TimeDebugPage
{
    [UsedImplicitly]
    public class TimeDebugPageInstaller : Installer<TimeDebugPageInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<TimeDebugPagePresenter>().AsSingle();
        }
    }
}
