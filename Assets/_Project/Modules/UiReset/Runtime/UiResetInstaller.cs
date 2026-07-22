using UnityEngine;
using Zenject;

namespace GameKit.UiReset
{
    [CreateAssetMenu(fileName = nameof(UiResetInstaller), menuName = "Installers/" + nameof(UiResetInstaller))]
    public class UiResetInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UiResetEventBus>().AsSingle();
        }
    }
}
