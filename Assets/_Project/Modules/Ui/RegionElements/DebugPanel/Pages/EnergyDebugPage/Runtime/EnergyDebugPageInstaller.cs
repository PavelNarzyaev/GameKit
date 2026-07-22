using JetBrains.Annotations;
using Zenject;

namespace GameKit.EnergyDebugPage
{
    [UsedImplicitly]
    public class EnergyDebugPageInstaller : Installer<EnergyDebugPageInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<EnergyDebugPagePresenter>().AsSingle();
        }
    }
}
