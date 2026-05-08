using GameKit.Core;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.CurrentTime
{
    [UsedImplicitly]
    public class CurrentTimeProvider
    {
        [Inject] private ICurrentTimeSource m_currentTimeSource;

        public long GetTimestamp()
        {
            return m_currentTimeSource.GetTimestamp();
        }
    }
}
