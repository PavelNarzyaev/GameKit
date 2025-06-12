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
        private static long s_startupTimestamp;

        [Inject]
        private void Inject()
        {
            var currentTimestamp = TimestampUtility.ConvertDatetimeToTimestamp(DateTime.UtcNow);
            s_startupTimestamp = currentTimestamp - (int)Time.realtimeSinceStartup;
        }

        public long GetTimestamp()
        {
            return s_startupTimestamp + (int)Time.realtimeSinceStartup;
        }
    }
}
