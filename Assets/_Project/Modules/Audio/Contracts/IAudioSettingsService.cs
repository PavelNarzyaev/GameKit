using System;

namespace GameKit.Audio.Contracts
{
    public interface IAudioSettingsService
    {
        event Action MusicEnabledChanged;
        event Action SoundEnabledChanged;

        bool IsMusicEnabled { get; }
        bool IsSoundEnabled { get; }

        void ToggleMusicEnabled();
        void ToggleSoundEnabled();
    }
}
