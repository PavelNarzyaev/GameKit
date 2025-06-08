using UnityEngine;
using Zenject;

namespace GameKit
{

	public class TimePageScreen : ScreenAbstract
	{
		[SerializeField] private DebugValue _currentTime;

		[Inject] private CurrentTimeProxy _currentTimeProxy;

		private void Start()
		{
			_currentTime.SetTitleText("Local Time");
			RefreshCurrentTime();
		}

		private void Update()
		{
			RefreshCurrentTime();
		}

		private void RefreshCurrentTime()
		{
			var timestamp = _currentTimeProxy.GetTimestamp();
			var readableDatetime = TimestampUtility.ConvertTimestampToReadableString(timestamp);
			_currentTime.SetValueText(readableDatetime);
		}
	}
}
