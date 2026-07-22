using UnityEngine;
using Zenject;

namespace GameKit.StateClipboardProxy
{
    [CreateAssetMenu(fileName = nameof(StateClipboardProxyInstaller), menuName = "Installers/" + nameof(StateClipboardProxyInstaller))]
    public class StateClipboardProxyInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<StateClipboardProxy>().AsSingle();
        }
    }
}
