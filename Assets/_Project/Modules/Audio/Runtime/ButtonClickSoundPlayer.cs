using System;
using GameKit.Audio.Contracts;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameKit.Audio
{
    public class ButtonClickSoundPlayer
    {
        private readonly IAudioConfig m_audioConfig;
        private AudioSource m_audioSource;

        public ButtonClickSoundPlayer(IAudioConfig audioConfig)
        {
            m_audioConfig = audioConfig;
        }

        public void Play()
        {
            EnsureAudioSourceCreated();
            m_audioSource.PlayOneShot(m_audioConfig.ButtonClick);
        }

        private void EnsureAudioSourceCreated()
        {
            if (m_audioSource != null)
            {
                return;
            }

            if (m_audioConfig.ButtonClick == null)
            {
                throw new InvalidOperationException("Button click clip is not configured.");
            }

            var gameObject = new GameObject(nameof(ButtonClickSoundPlayer));
            Object.DontDestroyOnLoad(gameObject);

            m_audioSource = gameObject.AddComponent<AudioSource>();
            m_audioSource.playOnAwake = false;
            m_audioSource.spatialBlend = 0f;
        }
    }
}
