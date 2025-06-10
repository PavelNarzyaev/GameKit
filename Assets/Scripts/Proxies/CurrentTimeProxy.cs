using System;
using JetBrains.Annotations;
using UnityEngine;
using Utilities;
using Zenject;

namespace Proxies
{
    [UsedImplicitly]
    public class CurrentTimeProxy
    {
        private static long _startupTimestamp;

        [Inject]
        private void Inject()
        {
            _startupTimestamp = TimestampUtility.ConvertDatetimeToTimestamp(DateTime.UtcNow) - (int)Time.realtimeSinceStartup;
        }

        public long GetTimestamp() => _startupTimestamp + (int)Time.realtimeSinceStartup;
    }
}
