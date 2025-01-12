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
	[Inject] private UserIdProxy _userIdProxy;
	[Inject] private FirstLaunchTimestampProxy _firstLaunchTimestampProxy;
	[Inject] private LaunchesCounterProxy _launchesCounterProxy;
	[Inject] private CurrentTimeProxy _currentTimeProxy;

	private void Start()
	{
		_launchCommand.Execute();
		_userIdText.text = _userIdProxy.Get();
		_firstLaunchTimeText.text = ConvertTimestampToReadableString(_firstLaunchTimestampProxy.Get());
		_launchCountText.text = _launchesCounterProxy.Get().ToString(CultureInfo.InvariantCulture);
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
