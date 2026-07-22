using JetBrains.Annotations;
using Zenject;

namespace GameKit.CurrenciesDebugPage
{
    [UsedImplicitly]
    public class CurrenciesDebugPageInstaller : Installer<CurrenciesDebugPageInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<CurrenciesDebugPagePresenter>().AsSingle();
        }
    }
}
