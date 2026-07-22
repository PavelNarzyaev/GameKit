using UnityEngine;
using Zenject;

namespace GameKit.Audio
{
    [CreateAssetMenu(fileName = nameof(AudioInstaller), menuName = "Installers/" + nameof(AudioInstaller))]
    public class AudioInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AudioSettingsService>().AsSingle();
            Container.Bind<AudioSourceFactory>().AsSingle();
            Container.Bind<AudioPlayer>().AsSingle();
            Container.BindInterfacesAndSelfTo<MusicPlayer>().AsSingle();
            Container.BindInterfacesAndSelfTo<SoundPlayer>().AsSingle();
        }
    }
}
