using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using Zenject;

public class Launcher : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _userIdText;
	[SerializeField] private TextMeshProUGUI _firstLaunchTimeText;
	[SerializeField] private TextMeshProUGUI _launchCountText;
	[SerializeField] private TextMeshProUGUI _currentTimeText;

	[Inject] private LaunchCommand _launchCommand;
	[Inject] private CurrentTimeProxy _currentTimeProxy;
	[Inject] private PersistentDataProxy _persistentDataProxy;

	private void Start()
	{
		// try
		// {
			_launchCommand.Execute();
		// }
		// catch (Exception e)
		// {
			// TODO: show launch error popup
			// throw;
		// }

		var persistentData = _persistentDataProxy.data;
		_userIdText.text = persistentData.userId;
		_firstLaunchTimeText.text = ConvertTimestampToReadableString(persistentData.firstLaunchTimestamp);
		_launchCountText.text = persistentData.launchesCounter.ToString(CultureInfo.InvariantCulture);
	}

	private void Update()
	{
		RefreshCurrentTimeText();
	}

	private void RefreshCurrentTimeText()
	{
		_currentTimeText.text = ConvertTimestampToReadableString(_currentTimeProxy.GetTimestamp());
	}

	private string ConvertTimestampToReadableString(long value)
	{
		return value.ToLocalDateTime().ToString("yyyy-MM-dd HH:mm:ss");
	}
}
