using UnityEngine;
using Zenject;

namespace GameKit.Currencies
{
    [CreateAssetMenu(fileName = nameof(CurrenciesInstaller), menuName = "Installers/" + nameof(CurrenciesInstaller))]
    public class CurrenciesInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerStateCurrenciesGateway>().AsSingle();
            Container.BindInterfacesAndSelfTo<CurrenciesService>().AsSingle();
        }
    }
}
