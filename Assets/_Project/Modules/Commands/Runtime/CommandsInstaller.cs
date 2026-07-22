using UnityEngine;
using Zenject;

namespace GameKit.Commands
{
    [CreateAssetMenu(fileName = nameof(CommandsInstaller), menuName = "Installers/" + nameof(CommandsInstaller))]
    public class CommandsInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LaunchCommand>().AsSingle();
            Container.BindInterfacesAndSelfTo<ShowInitialUiCommand>().AsSingle();
            Container.BindInterfacesAndSelfTo<ResetUiController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameKitTickController>().AsSingle();
            Container.BindInterfacesAndSelfTo<LogMessagesController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<EntryPointController>().AsSingle();
        }
    }
}
