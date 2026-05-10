using GameKit.UiDebugShared;
using GameKit.UiRegions;
using UnityEngine;
using Zenject;

namespace GameKit.TimeDebugPage
{
    public class TimeDebugPageView : UiRegionElement
    {
        [SerializeField] private DebugValue currentTime;
        [SerializeField] private DebugValue timeOffset;
        [SerializeField] private DebugButton addHourButton;
        [SerializeField] private DebugButton addMinuteButton;
        [SerializeField] private DebugButton addSecondButton;

        [Inject] private TimeDebugPagePresenter m_presenter;

        private void Awake()
        {
            addHourButton.AddClickListener(m_presenter.AddHour);
            addMinuteButton.AddClickListener(m_presenter.AddMinute);
            addSecondButton.AddClickListener(m_presenter.AddSecond);
        }

        private void OnEnable()
        {
            Refresh();
            m_presenter.Changed += HandleChanged;
        }

        private void Update()
        {
            RefreshCurrentTime();
        }

        private void OnDisable()
        {
            m_presenter.Changed -= HandleChanged;
        }

        private void HandleChanged()
        {
            Refresh();
        }

        private void Refresh()
        {
            RefreshCurrentTime();
            RefreshTimeOffset();
        }

        private void RefreshCurrentTime()
        {
            currentTime.SetValueText(m_presenter.GetCurrentTimeText());
        }

        private void RefreshTimeOffset()
        {
            timeOffset.SetValueText(m_presenter.GetTimeOffset());
        }
    }
}
