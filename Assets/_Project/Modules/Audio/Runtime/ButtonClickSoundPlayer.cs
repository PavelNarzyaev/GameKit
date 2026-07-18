using System;
using GameKit.Audio.Contracts;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameKit.Audio
{
    [UsedImplicitly]
    public class ButtonClickSoundPlayer
    {
        private readonly IAudioConfig m_audioConfig;
        private readonly IAudioSettingsService m_audioSettingsService;
        private AudioSource m_audioSource;

        public ButtonClickSoundPlayer(IAudioConfig audioConfig, IAudioSettingsService audioSettingsService)
        {
            m_audioConfig = audioConfig;
            m_audioSettingsService = audioSettingsService;
        }

        public void Play()
        {
            if (!m_audioSettingsService.IsSoundEnabled)
            {
                return;
            }

            EnsureAudioSourceCreated();
            m_audioSource.PlayOneShot(m_audioConfig.ButtonClick);
        }

        private void EnsureAudioSourceCreated()
        {
            if (m_audioSource)
            {
                return;
            }

            if (!m_audioConfig.ButtonClick)
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
