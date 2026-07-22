using UnityEngine;
using Zenject;

namespace GameKit.UiPopups
{
    [CreateAssetMenu(fileName = nameof(UiPopupsInstaller), menuName = "Installers/" + nameof(UiPopupsInstaller))]
    public class UiPopupsInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PopupNavigator>().AsSingle();
            Container.BindInterfacesAndSelfTo<PopupPresenter>().AsSingle();
            Container.Bind<PopupBackdropPresenter>().AsSingle();
        }
    }
}
