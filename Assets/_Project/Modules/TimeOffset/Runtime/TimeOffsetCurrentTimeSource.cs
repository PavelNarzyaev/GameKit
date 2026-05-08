using GameKit.Core;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.TimeOffset
{
    [UsedImplicitly]
    public class TimeOffsetCurrentTimeSource : ICurrentTimeSource
    {
        [Inject(Id = CurrentTimeSourceIds.k_BaseCurrentTimeSource)]
        private ICurrentTimeSource m_baseCurrentTimeSource;

        [Inject] private TimeOffsetService m_timeOffsetService;

        public long GetTimestamp()
        {
            return m_baseCurrentTimeSource.GetTimestamp() + m_timeOffsetService.OffsetSeconds;
        }
    }
}
