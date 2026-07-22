using GameKit.Audio.Contracts;
using JetBrains.Annotations;
using UnityEngine;

namespace GameKit.Audio
{
    [UsedImplicitly]
    public class SoundPlayer : ISoundPlayer
    {
        private readonly AudioPlayer m_audioPlayer;
        private readonly AudioSourceFactory m_audioSourceFactory;
        private readonly IAudioSettingsService m_audioSettingsService;
        private AudioSource m_audioSource;

        public SoundPlayer(
            AudioPlayer audioPlayer,
            AudioSourceFactory audioSourceFactory,
            IAudioSettingsService audioSettingsService)
        {
            m_audioPlayer = audioPlayer;
            m_audioSourceFactory = audioSourceFactory;
            m_audioSettingsService = audioSettingsService;
        }

        public void Play(AudioClip clip)
        {
            m_audioPlayer.EnsureClipConfigured(clip);

            if (!m_audioSettingsService.IsSoundEnabled.CurrentValue)
            {
                return;
            }

            EnsureAudioSourceCreated();
            m_audioPlayer.PlayOneShot(m_audioSource, clip);
        }

        private void EnsureAudioSourceCreated()
        {
            if (m_audioSource)
            {
                return;
            }

            m_audioSource = m_audioSourceFactory.Create(nameof(SoundPlayer));
        }
    }
}
