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
            var currentTimestamp = TimestampUtility.ConvertDatetimeToTimestamp(DateTime.UtcNow);
            _startupTimestamp = currentTimestamp - (int)Time.realtimeSinceStartup;
        }

        public long GetTimestamp()
        {
            return _startupTimestamp + (int)Time.realtimeSinceStartup;
        }
    }
}
