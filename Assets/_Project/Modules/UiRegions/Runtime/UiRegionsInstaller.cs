using UnityEngine;
using Zenject;

namespace GameKit.UiRegions
{
    [CreateAssetMenu(fileName = nameof(UiRegionsInstaller), menuName = "Installers/" + nameof(UiRegionsInstaller))]
    public class UiRegionsInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<UiRegionElementSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<UiRegionHostPresenter>().AsSingle();
        }
    }
}
