using UnityEngine;
using Zenject;

namespace GameKit.Energy
{
    [CreateAssetMenu(fileName = nameof(EnergyInstaller), menuName = "Installers/" + nameof(EnergyInstaller))]
    public class EnergyInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerStateEnergyGateway>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnergyService>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnergyRestorationController>().AsSingle().NonLazy();
        }
    }
}
