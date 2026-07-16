using System;
using GameKit.Audio.Contracts;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameKit.Audio
{
    public class BackgroundMusicPlayer
    {
        private readonly IAudioConfig m_audioConfig;
        private AudioSource m_audioSource;

        public BackgroundMusicPlayer(IAudioConfig audioConfig)
        {
            m_audioConfig = audioConfig;
        }

        public void Play()
        {
            EnsureAudioSourceCreated();
            if (m_audioSource.isPlaying)
            {
                return;
            }

            m_audioSource.Play();
        }

        private void EnsureAudioSourceCreated()
        {
            if (m_audioSource != null)
            {
                return;
            }

            if (m_audioConfig.BackgroundMusic == null)
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
