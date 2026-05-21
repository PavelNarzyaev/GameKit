using GameKit.Core.Contracts;
using GameKit.TimeOffset.Contracts;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.TimeOffset
{
    [UsedImplicitly]
    public class TimeOffsetCurrentTimeSource : ICurrentTimeSource
    {
        private readonly ICurrentTimeSource m_baseCurrentTimeSource;
        private readonly LazyInject<ITimeOffsetService> m_timeOffsetService;

        public TimeOffsetCurrentTimeSource(
            [Inject(Id = CurrentTimeSourceIds.k_BaseCurrentTimeSource)] ICurrentTimeSource baseCurrentTimeSource,
            LazyInject<ITimeOffsetService> timeOffsetService)
        {
            m_baseCurrentTimeSource = baseCurrentTimeSource;
            m_timeOffsetService = timeOffsetService;
        }

        public long GetTimestamp()
        {
            return m_baseCurrentTimeSource.GetTimestamp() + m_timeOffsetService.Value.OffsetSeconds;
        }
    }
}
