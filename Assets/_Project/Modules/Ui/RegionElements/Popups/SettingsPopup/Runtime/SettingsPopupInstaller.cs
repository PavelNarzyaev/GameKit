using UnityEngine;
using Zenject;

namespace GameKit.SettingsPopup
{
    [CreateAssetMenu(fileName = nameof(SettingsPopupInstaller), menuName = "Installers/" + nameof(SettingsPopupInstaller))]
    public class SettingsPopupInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SettingsPopupPresenter>().AsSingle();
        }
    }
}
