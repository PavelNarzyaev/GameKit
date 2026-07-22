using System;
using GameKit.UiPopups;
using R3;
using UnityEngine;
using Zenject;

namespace GameKit.SettingsPopup
{
    public class SettingsPopupView : PopupView
    {
        [SerializeField] private Checkbox musicCheckbox;
        [SerializeField] private Checkbox soundCheckbox;
        [Inject] private SettingsPopupPresenter m_presenter;
        private IDisposable m_musicEnabledSubscription;
        private IDisposable m_soundEnabledSubscription;

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
            m_musicEnabledSubscription = m_presenter.IsMusicEnabled.Subscribe(RefreshMusicCheckbox);
            m_soundEnabledSubscription = m_presenter.IsSoundEnabled.Subscribe(RefreshSoundCheckbox);
        }

        protected override void OnDisable()
        {
            m_musicEnabledSubscription.Dispose();
            m_soundEnabledSubscription.Dispose();
            base.OnDisable();
        }

        private void RefreshMusicCheckbox(bool isMusicEnabled)
        {
            musicCheckbox.SetIsOn(isMusicEnabled);
        }

        private void RefreshSoundCheckbox(bool isSoundEnabled)
        {
            soundCheckbox.SetIsOn(isSoundEnabled);
        }
    }
}
