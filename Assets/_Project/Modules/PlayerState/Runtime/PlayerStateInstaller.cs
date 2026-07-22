using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace GameKit.PlayerState
{
    [UsedImplicitly]
    [CreateAssetMenu(fileName = nameof(PlayerStateInstaller), menuName = "Installers/" + nameof(PlayerStateInstaller))]
    public class PlayerStateInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            InstallRuntimeStorage(Container);
            InstallCore(Container);
            InstallAutoSave(Container);
        }

        public static void InstallRuntimeStorage(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<FilePlayerStateStorage>().AsSingle();
            container.BindInterfacesAndSelfTo<EncryptionKeysProvider>().AsSingle();
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
