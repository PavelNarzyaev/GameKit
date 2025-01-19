using UnityEngine;
using Zenject;

public class TimePageScreen : MonoBehaviour
{
	[SerializeField] private DebugValue _currentTime;

	[Inject] private CurrentTimeProxy _currentTimeProxy;

	private void Start()
	{
		_currentTime.SetTitleText("Local Time");
	}

	private void Update()
	{
		var timestamp = _currentTimeProxy.GetTimestamp();
		var readableDatetime = TimestampUtility.ConvertTimestampToReadableString(timestamp);
		_currentTime.SetValueText(readableDatetime);
	}
}
