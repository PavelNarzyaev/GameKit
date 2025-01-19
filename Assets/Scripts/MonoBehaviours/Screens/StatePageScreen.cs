using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StatePageScreen : MonoBehaviour
{
	[SerializeField] private DebugValue _userId;
	[SerializeField] private DebugValue _firstLaunchTime;
	[SerializeField] private DebugValue _launchCount;
	[SerializeField] private Button _copyToClipboardButton;
	[SerializeField] private Button _pasteFromClipboardButton;

	[Inject] private StateProxy _stateProxy;
	[Inject] private StateClipboardProxy _stateClipboardProxy;

	private void Awake()
	{
		_copyToClipboardButton.onClick.AddListener(_stateClipboardProxy.CopyStateToClipboard);
		_pasteFromClipboardButton.onClick.AddListener(_stateClipboardProxy.PasteStateFromClipboard);
	}

	private void Start()
	{
		var state = _stateProxy.data;

		_userId.SetTitleText("USER ID");
		_userId.SetValueText(state.userId);

		_firstLaunchTime.SetTitleText("FIRST LAUNCH");
		_firstLaunchTime.SetValueText(ConvertTimestampToReadableString(state.firstLaunchTimestamp));

		_launchCount.SetTitleText("LAUNCH COUNT");
		_launchCount.SetValueText(state.launchesCounter.ToString(CultureInfo.InvariantCulture));
	}

	private string ConvertTimestampToReadableString(long value)
	{
		return value.ToLocalDateTime().ToString("yyyy-MM-dd HH:mm:ss");
	}
}
