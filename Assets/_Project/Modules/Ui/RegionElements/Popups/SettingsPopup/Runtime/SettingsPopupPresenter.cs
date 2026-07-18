using System;
using GameKit.Audio.Contracts;
using JetBrains.Annotations;

namespace GameKit.SettingsPopup
{
    [UsedImplicitly]
    public class SettingsPopupPresenter : IDisposable
    {
        private readonly IAudioSettingsService m_audioSettingsService;

        public event Action MusicEnabledChanged;
        public event Action SoundEnabledChanged;

        public bool IsMusicEnabled => m_audioSettingsService.IsMusicEnabled;
        public bool IsSoundEnabled => m_audioSettingsService.IsSoundEnabled;

        public SettingsPopupPresenter(IAudioSettingsService audioSettingsService)
        {
            m_audioSettingsService = audioSettingsService;
            m_audioSettingsService.MusicEnabledChanged += HandleMusicEnabledChanged;
            m_audioSettingsService.SoundEnabledChanged += HandleSoundEnabledChanged;
        }

        public void Dispose()
        {
            m_audioSettingsService.MusicEnabledChanged -= HandleMusicEnabledChanged;
            m_audioSettingsService.SoundEnabledChanged -= HandleSoundEnabledChanged;
        }

        public void ToggleMusicEnabled()
        {
            m_audioSettingsService.ToggleMusicEnabled();
        }

        public void ToggleSoundEnabled()
        {
            m_audioSettingsService.ToggleSoundEnabled();
        }

        private void HandleMusicEnabledChanged()
        {
            MusicEnabledChanged?.Invoke();
        }

        private void HandleSoundEnabledChanged()
        {
            SoundEnabledChanged?.Invoke();
        }
    }
}
