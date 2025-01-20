using System.Globalization;
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
	[SerializeField] private Button _resetButton;

	[Inject] private StateProxy _stateProxy;
	[Inject] private StateClipboardProxy _stateClipboardProxy;
	[Inject] private ResetStateCommand _resetStateCommand;

	private void Awake()
	{
		_copyToClipboardButton.onClick.AddListener(_stateClipboardProxy.CopyStateToClipboard);
		_pasteFromClipboardButton.onClick.AddListener(_stateClipboardProxy.PasteStateFromClipboard);
		_resetButton.onClick.AddListener(_resetStateCommand.Execute);
	}

	private void Start()
	{
		var state = _stateProxy.data;

		_userId.SetTitleText("User Id");
		_userId.SetValueText(state.userId);

		_firstLaunchTime.SetTitleText("First Launch");
		_firstLaunchTime.SetValueText(TimestampUtility.ConvertTimestampToReadableString(state.firstLaunchTimestamp));

		_launchCount.SetTitleText("Launch Count");
		_launchCount.SetValueText(state.launchesCounter.ToString(CultureInfo.InvariantCulture));
	}
}
