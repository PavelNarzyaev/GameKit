using UnityEngine;
using Zenject;

namespace GameKit.UiPages
{
    [CreateAssetMenu(fileName = nameof(UiPagesInstaller), menuName = "Installers/" + nameof(UiPagesInstaller))]
    public class UiPagesInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PageNavigator>().AsSingle();
        }
    }
}
