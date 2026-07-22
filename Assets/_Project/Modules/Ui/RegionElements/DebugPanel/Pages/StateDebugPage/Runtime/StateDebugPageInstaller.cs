using JetBrains.Annotations;
using Zenject;

namespace GameKit.StateDebugPage
{
    [UsedImplicitly]
    public class StateDebugPageInstaller : Installer<StateDebugPageInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<StateDebugPagePresenter>().AsSingle();
        }
    }
}
