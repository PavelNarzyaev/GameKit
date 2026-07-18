using GameKit.UiPopups;
using UnityEngine;
using Zenject;

namespace GameKit.SettingsPopup
{
    public class SettingsPopupView : PopupView
    {
        [SerializeField] private Checkbox musicCheckbox;
        [SerializeField] private Checkbox soundCheckbox;
        [Inject] private SettingsPopupPresenter m_presenter;

        private void Awake()
        {
            musicCheckbox.Clicked += m_presenter.ToggleMusicEnabled;
            soundCheckbox.Clicked += m_presenter.ToggleSoundEnabled;
        }

        private void OnDestroy()
        {
            musicCheckbox.Clicked -= m_presenter.ToggleMusicEnabled;
            soundCheckbox.Clicked -= m_presenter.ToggleSoundEnabled;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Refresh();
            m_presenter.MusicEnabledChanged += HandleMusicEnabledChanged;
            m_presenter.SoundEnabledChanged += HandleSoundEnabledChanged;
        }

        protected override void OnDisable()
        {
            m_presenter.MusicEnabledChanged -= HandleMusicEnabledChanged;
            m_presenter.SoundEnabledChanged -= HandleSoundEnabledChanged;
            base.OnDisable();
        }

        private void HandleMusicEnabledChanged()
        {
            musicCheckbox.SetIsOn(m_presenter.IsMusicEnabled);
        }

        private void HandleSoundEnabledChanged()
        {
            soundCheckbox.SetIsOn(m_presenter.IsSoundEnabled);
        }

        private void Refresh()
        {
            musicCheckbox.SetIsOn(m_presenter.IsMusicEnabled);
            soundCheckbox.SetIsOn(m_presenter.IsSoundEnabled);
        }
    }
}
