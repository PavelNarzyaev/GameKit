using System;
using System.Globalization;
using GameKit.Core;
using GameKit.CurrentTime;
using GameKit.TimeOffset;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.TimeDebugPage
{
    [UsedImplicitly]
    public class TimeDebugPagePresenter
    {
        [Inject] private CurrentTimeProvider m_currentTimeProvider;
        [Inject] private TimeOffsetService m_timeOffsetService;

        public event Action Changed
        {
            add => m_timeOffsetService.Changed += value;
            remove => m_timeOffsetService.Changed -= value;
        }

        public string GetCurrentTimeText()
        {
            var timestamp = m_currentTimeProvider.GetTimestamp();
            return timestamp.ToLocalDatetimeString();
        }

        public string GetTimeOffset()
        {
            var offsetSeconds = m_timeOffsetService.OffsetSeconds;
            var absoluteSeconds = Math.Abs((long)offsetSeconds);
            var totalHours = absoluteSeconds / 3600;
            var minutes = absoluteSeconds / 60 % 60;
            var seconds = absoluteSeconds % 60;
            var sign = offsetSeconds < 0 ? "-" : "+";

            return string.Format(
                CultureInfo.InvariantCulture,
                "{0}{1:00}:{2:00}:{3:00}",
                sign,
                totalHours,
                minutes,
                seconds);
        }

        public void AddHour()
        {
            m_timeOffsetService.AddSeconds(3600);
        }

        public void AddMinute()
        {
            m_timeOffsetService.AddSeconds(60);
        }

        public void AddSecond()
        {
            m_timeOffsetService.AddSeconds(1);
        }
    }
}
