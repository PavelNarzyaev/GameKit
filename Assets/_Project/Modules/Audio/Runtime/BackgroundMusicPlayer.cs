using System;
using GameKit.Audio.Contracts;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameKit.Audio
{
    [UsedImplicitly]
    public class BackgroundMusicPlayer : IDisposable
    {
        private readonly IAudioConfig m_audioConfig;
        private readonly IAudioSettingsService m_audioSettingsService;
        private AudioSource m_audioSource;

        public BackgroundMusicPlayer(IAudioConfig audioConfig, IAudioSettingsService audioSettingsService)
        {
            m_audioConfig = audioConfig;
            m_audioSettingsService = audioSettingsService;
            m_audioSettingsService.MusicEnabledChanged += HandleMusicEnabledChanged;
        }

        public void Play()
        {
            EnsureAudioSourceCreated();
            RefreshPlayback();
        }

        public void Dispose()
        {
            m_audioSettingsService.MusicEnabledChanged -= HandleMusicEnabledChanged;
        }

        private void HandleMusicEnabledChanged()
        {
            if (!m_audioSource)
            {
                return;
            }

            RefreshPlayback();
        }

        private void RefreshPlayback()
        {
            if (m_audioSettingsService.IsMusicEnabled)
            {
                PlayIfNeeded();
                return;
            }

            PauseIfNeeded();
        }

        private void PlayIfNeeded()
        {
            if (m_audioSource.isPlaying)
            {
                return;
            }

            m_audioSource.Play();
        }

        private void PauseIfNeeded()
        {
            if (!m_audioSource.isPlaying)
            {
                return;
            }

            m_audioSource.Pause();
        }

        private void EnsureAudioSourceCreated()
        {
            if (m_audioSource)
            {
                return;
            }

            if (!m_audioConfig.BackgroundMusic)
            {
                throw new InvalidOperationException("Background music clip is not configured.");
            }

            var gameObject = new GameObject(nameof(BackgroundMusicPlayer));
            Object.DontDestroyOnLoad(gameObject);

            m_audioSource = gameObject.AddComponent<AudioSource>();
            m_audioSource.clip = m_audioConfig.BackgroundMusic;
            m_audioSource.loop = true;
            m_audioSource.playOnAwake = false;
        }
    }
}
