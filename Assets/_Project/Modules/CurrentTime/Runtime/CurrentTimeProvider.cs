using GameKit.Core.Contracts;
using GameKit.CurrentTime.Contracts;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.CurrentTime
{
    [UsedImplicitly]
    public class CurrentTimeProvider : ICurrentTimeProvider
    {
        [Inject] private ICurrentTimeSource m_currentTimeSource;

        public long GetTimestamp()
        {
            return m_currentTimeSource.GetTimestamp();
        }
    }
}
