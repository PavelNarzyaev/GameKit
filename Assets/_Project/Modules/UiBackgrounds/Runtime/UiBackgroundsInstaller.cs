using UnityEngine;
using Zenject;

namespace GameKit.UiBackgrounds
{
    [CreateAssetMenu(fileName = nameof(UiBackgroundsInstaller), menuName = "Installers/" + nameof(UiBackgroundsInstaller))]
    public class UiBackgroundsInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BackgroundNavigator>().AsSingle();
        }
    }
}
