using GameKit.Core.Contracts;
using GameKit.TimeOffset.Contracts;
using JetBrains.Annotations;

namespace GameKit.TimeOffset
{
    [UsedImplicitly]
    public class TimeOffsetCurrentTimeSource : ICurrentTimeSource
    {
        private readonly IRealTimeSource m_realTimeSource;
        private readonly ITimeOffsetService m_timeOffsetService;

        public TimeOffsetCurrentTimeSource(IRealTimeSource realTimeSource, ITimeOffsetService timeOffsetService)
        {
            m_realTimeSource = realTimeSource;
            m_timeOffsetService = timeOffsetService;
        }

        public long GetTimestamp()
        {
            return m_realTimeSource.GetTimestamp() + m_timeOffsetService.OffsetSeconds;
        }
    }
}
