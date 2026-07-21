using System;
using GameKit.Audio.Contracts;
using JetBrains.Annotations;
using UnityEngine;

namespace GameKit.Audio
{
    [UsedImplicitly]
    public class MusicPlayer : IMusicPlayer, IDisposable
    {
        private readonly AudioPlayer m_audioPlayer;
        private readonly AudioSourceFactory m_audioSourceFactory;
        private readonly IAudioSettingsService m_audioSettingsService;
        private AudioClip m_currentClip;
        private AudioSource m_audioSource;
        private bool m_isPaused;

        public MusicPlayer(
            AudioPlayer audioPlayer,
            AudioSourceFactory audioSourceFactory,
            IAudioSettingsService audioSettingsService)
        {
            m_audioPlayer = audioPlayer;
            m_audioSourceFactory = audioSourceFactory;
            m_audioSettingsService = audioSettingsService;
            m_audioSettingsService.MusicEnabledChanged += HandleMusicEnabledChanged;
        }

        public void Play(AudioClip clip)
        {
            m_audioPlayer.EnsureClipConfigured(clip);
            m_currentClip = clip;
            EnsureAudioSourceCreated();
            RefreshPlayback();
        }

        public void Stop()
        {
            m_currentClip = null;
            m_isPaused = false;

            if (!m_audioSource)
            {
                return;
            }

            m_audioPlayer.Stop(m_audioSource);
        }

        public void Dispose()
        {
            m_audioSettingsService.MusicEnabledChanged -= HandleMusicEnabledChanged;
        }

        private void HandleMusicEnabledChanged()
        {
            if (!m_audioSource || !m_currentClip)
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
            if (m_isPaused && m_audioSource.clip == m_currentClip)
            {
                m_audioPlayer.UnPause(m_audioSource);
                m_isPaused = false;
                return;
            }

            m_audioPlayer.PlayLooped(m_audioSource, m_currentClip);
            m_isPaused = false;
        }

        private void PauseIfNeeded()
        {
            if (!m_audioSource.isPlaying)
            {
                return;
            }

            m_audioPlayer.Pause(m_audioSource);
            m_isPaused = true;
        }

        private void EnsureAudioSourceCreated()
        {
            if (m_audioSource)
            {
                return;
            }

            m_audioSource = m_audioSourceFactory.Create(nameof(MusicPlayer));
        }
    }
}
