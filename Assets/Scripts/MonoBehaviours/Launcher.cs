using System;
using TMPro;
using UnityEngine;
using Zenject;

public class Launcher : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _temporaryUserIdText;
	[SerializeField] private TextMeshProUGUI _temporaryFirstLaunchTimeText;
	[SerializeField] private TextMeshProUGUI _temporaryCurrentTimeText;

	[Inject] private LaunchCommand _launchCommand;
	[Inject] private UserIdProxy _userIdProxy;
	[Inject] private FirstLaunchTimestampProxy _firstLaunchTimestampProxy;
	[Inject] private CurrentTimeProxy _currentTimeProxy;

	private void Start()
	{
		_launchCommand.Execute();
		_temporaryUserIdText.text = _userIdProxy.Get();
		_temporaryFirstLaunchTimeText.text = ConvertTimestampToReadableString(_firstLaunchTimestampProxy.Get());
	}

	private void Update()
	{
		RefreshCurrentTimeText();
	}

	private void RefreshCurrentTimeText()
	{
		_temporaryCurrentTimeText.text = ConvertTimestampToReadableString(_currentTimeProxy.GetTimestamp());
	}

	private string ConvertTimestampToReadableString(long value)
	{
		return value.ToLocalDateTime().ToString("yyyy-MM-dd HH:mm:ss");
	}
}
