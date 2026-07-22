using R3;

namespace GameKit.Audio.Contracts
{
    public interface IAudioSettingsService
    {
        ReadOnlyReactiveProperty<bool> IsMusicEnabled { get; }
        ReadOnlyReactiveProperty<bool> IsSoundEnabled { get; }

        void ToggleMusicEnabled();
        void ToggleSoundEnabled();
    }
}
