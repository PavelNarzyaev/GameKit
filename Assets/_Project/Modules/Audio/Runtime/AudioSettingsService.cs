using System;
using GameKit.Audio.Contracts;
using JetBrains.Annotations;

namespace GameKit.Audio
{
    [UsedImplicitly]
    public class AudioSettingsService : IAudioSettingsService
    {
        public event Action MusicEnabledChanged;
        public event Action SoundEnabledChanged;

        public bool IsMusicEnabled { get; private set; } = true;

        public bool IsSoundEnabled { get; private set; } = true;

        public void ToggleMusicEnabled()
        {
            SetMusicEnabled(!IsMusicEnabled);
        }

        public void ToggleSoundEnabled()
        {
            SetSoundEnabled(!IsSoundEnabled);
        }

        public void SetMusicEnabled(bool isEnabled)
        {
            if (IsMusicEnabled == isEnabled)
            {
                return;
            }

            IsMusicEnabled = isEnabled;
            MusicEnabledChanged?.Invoke();
        }

        public void SetSoundEnabled(bool isEnabled)
        {
            if (IsSoundEnabled == isEnabled)
            {
                return;
            }

            IsSoundEnabled = isEnabled;
            SoundEnabledChanged?.Invoke();
        }
    }
}
