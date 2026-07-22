using UnityEngine;
using Zenject;

namespace GameKit.UiFonts
{
    [CreateAssetMenu(fileName = nameof(UiFontsInstaller), menuName = "Installers/" + nameof(UiFontsInstaller))]
    public class UiFontsInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UiFontPreloader>().AsSingle();
        }
    }
}
