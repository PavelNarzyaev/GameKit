using GameKit.Audio.Contracts;
using JetBrains.Annotations;
using R3;

namespace GameKit.SettingsPopup
{
    [UsedImplicitly]
    public class SettingsPopupPresenter
    {
        private readonly IAudioSettingsService m_audioSettingsService;

        public ReadOnlyReactiveProperty<bool> IsMusicEnabled => m_audioSettingsService.IsMusicEnabled;
        public ReadOnlyReactiveProperty<bool> IsSoundEnabled => m_audioSettingsService.IsSoundEnabled;

        public SettingsPopupPresenter(IAudioSettingsService audioSettingsService)
        {
            m_audioSettingsService = audioSettingsService;
        }

        public void ToggleMusicEnabled()
        {
            m_audioSettingsService.ToggleMusicEnabled();
        }

        public void ToggleSoundEnabled()
        {
            m_audioSettingsService.ToggleSoundEnabled();
        }
    }
}
