using UnityEngine;
using Zenject;

namespace GameKit.Logs
{
    [CreateAssetMenu(fileName = nameof(LogsInstaller), menuName = "Installers/" + nameof(LogsInstaller))]
    public class LogsInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LogsProvider>().AsSingle();
        }
    }
}
