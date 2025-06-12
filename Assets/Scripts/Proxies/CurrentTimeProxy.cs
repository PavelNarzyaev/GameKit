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
        private long m_startupTimestamp;

        [Inject]
        private void Inject()
        {
            var currentTimestamp = TimestampUtility.ConvertDatetimeToTimestamp(DateTime.UtcNow);
            m_startupTimestamp = currentTimestamp - (int)Time.realtimeSinceStartup;
        }

        public long GetTimestamp()
        {
            return m_startupTimestamp + (int)Time.realtimeSinceStartup;
        }
    }
}
