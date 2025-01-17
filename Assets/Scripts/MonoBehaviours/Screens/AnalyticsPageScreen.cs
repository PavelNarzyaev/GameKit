using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AnalyticsPageScreen : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _userIdText;
	[SerializeField] private TextMeshProUGUI _firstLaunchTimeText;
	[SerializeField] private TextMeshProUGUI _launchCountText;
	[SerializeField] private TextMeshProUGUI _currentTimeText;
	[SerializeField] private Button _copyToClipboardButton;
	[SerializeField] private Button _pasteFromClipboardButton;

	[Inject] private CurrentTimeProxy _currentTimeProxy;
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
		_userIdText.text = state.userId;
		_firstLaunchTimeText.text = ConvertTimestampToReadableString(state.firstLaunchTimestamp);
		_launchCountText.text = state.launchesCounter.ToString(CultureInfo.InvariantCulture);
	}

	private void Update()
	{
		_currentTimeText.text = ConvertTimestampToReadableString(_currentTimeProxy.GetTimestamp());
	}

	private string ConvertTimestampToReadableString(long value)
	{
		return value.ToLocalDateTime().ToString("yyyy-MM-dd HH:mm:ss");
	}
}
