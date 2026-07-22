using UnityEngine;
using Zenject;

namespace GameKit.ErrorPopup
{
    [CreateAssetMenu(fileName = nameof(ErrorPopupInstaller), menuName = "Installers/" + nameof(ErrorPopupInstaller))]
    public class ErrorPopupInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ErrorPopupPresenter>().AsSingle();
        }
    }
}
