using System;
using GameKit.Audio.Contracts;
using JetBrains.Annotations;
using R3;
using UnityEngine;

namespace GameKit.Audio
{
    [UsedImplicitly]
    public class AudioSettingsService : IAudioSettingsService, IDisposable
    {
        private const string k_MusicEnabledKey = "GameKit.Audio.MusicEnabled";
        private const string k_SoundEnabledKey = "GameKit.Audio.SoundEnabled";

        private readonly ReactiveProperty<bool> m_isMusicEnabled = new(LoadBool(k_MusicEnabledKey, true));
        private readonly ReactiveProperty<bool> m_isSoundEnabled = new(LoadBool(k_SoundEnabledKey, true));

        public ReadOnlyReactiveProperty<bool> IsMusicEnabled => m_isMusicEnabled;

        public ReadOnlyReactiveProperty<bool> IsSoundEnabled => m_isSoundEnabled;

        public void ToggleMusicEnabled()
        {
            SetMusicEnabled(!m_isMusicEnabled.CurrentValue);
        }

        public void ToggleSoundEnabled()
        {
            SetSoundEnabled(!m_isSoundEnabled.CurrentValue);
        }

        public void Dispose()
        {
            m_isMusicEnabled.Dispose();
            m_isSoundEnabled.Dispose();
        }

        private void SetMusicEnabled(bool isEnabled)
        {
            if (m_isMusicEnabled.CurrentValue == isEnabled)
            {
                return;
            }

            SaveBool(k_MusicEnabledKey, isEnabled);
            m_isMusicEnabled.Value = isEnabled;
        }

        private void SetSoundEnabled(bool isEnabled)
        {
            if (m_isSoundEnabled.CurrentValue == isEnabled)
            {
                return;
            }

            SaveBool(k_SoundEnabledKey, isEnabled);
            m_isSoundEnabled.Value = isEnabled;
        }

        private static bool LoadBool(string key, bool defaultValue)
        {
            return PlayerPrefs.GetInt(key, BoolToInt(defaultValue)) != 0;
        }

        private static void SaveBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, BoolToInt(value));
            PlayerPrefs.Save();
        }

        private static int BoolToInt(bool value)
        {
            return value ? 1 : 0;
        }
    }
}
