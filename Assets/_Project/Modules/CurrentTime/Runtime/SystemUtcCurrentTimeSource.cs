using System;
using GameKit.Core.Contracts;
using JetBrains.Annotations;

namespace GameKit.CurrentTime
{
    [UsedImplicitly]
    public class SystemUtcCurrentTimeSource : ICurrentTimeSource
    {
        public long GetTimestamp()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }
}
