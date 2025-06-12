using Proxies;
using UnityEngine;
using Utilities;
using Zenject;

namespace MonoBehaviours.Screens
{
    public class TimePageScreen : ScreenAbstract
    {
        [SerializeField] private DebugValue currentTime;

        [Inject] private CurrentTimeProxy m_currentTimeProxy;

        private void Start()
        {
            currentTime.SetTitleText("Local Time");
            RefreshCurrentTime();
        }

        private void Update()
        {
            RefreshCurrentTime();
        }

        private void RefreshCurrentTime()
        {
            var timestamp = m_currentTimeProxy.GetTimestamp();
            var readableDatetime = TimestampUtility.ConvertTimestampToReadableString(timestamp);
            currentTime.SetValueText(readableDatetime);
        }
    }
}
