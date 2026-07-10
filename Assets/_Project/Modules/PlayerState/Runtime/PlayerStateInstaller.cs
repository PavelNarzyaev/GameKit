using JetBrains.Annotations;
using Zenject;

namespace GameKit.PlayerState
{
    [UsedImplicitly]
    public class PlayerStateInstaller : Installer<PlayerStateInstaller>
    {
        public override void InstallBindings()
        {
            InstallCore(Container);
            InstallAutoSave(Container);
        }

        public static void InstallCore(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<JsonPlayerStateSerializer>().AsSingle();
            container.BindInterfacesAndSelfTo<AesTextCipher>().AsSingle();
            container.BindInterfacesAndSelfTo<FilePlayerStateCodec>().AsSingle();
            container.BindInterfacesAndSelfTo<PlayerStateValidator>().AsSingle();
            container.BindInterfacesAndSelfTo<PlayerStateFactory>().AsSingle();
            container.BindInterfacesAndSelfTo<PlayerStateProvider>().AsSingle();
        }

        public static void InstallAutoSave(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<PlayerStateSavingController>().AsSingle().NonLazy();
        }
    }
}
