using GameKit.Core.Contracts;
using GameKit.CurrentTime.Contracts;
using JetBrains.Annotations;

namespace GameKit.CurrentTime
{
    [UsedImplicitly]
    public class CurrentTimeProvider : ICurrentTimeProvider
    {
        private readonly ICurrentTimeSource m_currentTimeSource;

        public CurrentTimeProvider(ICurrentTimeSource currentTimeSource)
        {
            m_currentTimeSource = currentTimeSource;
        }

        public long GetTimestamp()
        {
            return m_currentTimeSource.GetTimestamp();
        }
    }
}
