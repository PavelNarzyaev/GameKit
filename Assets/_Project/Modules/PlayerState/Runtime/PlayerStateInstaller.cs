using JetBrains.Annotations;
using Zenject;

namespace GameKit.PlayerState
{
    [UsedImplicitly]
    public class PlayerStateInstaller : Installer<PlayerStateInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<JsonPlayerStateSerializer>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerStateValidator>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerStateFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerStateProvider>().AsSingle();
            Container.Bind<PlayerStateSavingController>().AsSingle().NonLazy();
        }
    }
}
