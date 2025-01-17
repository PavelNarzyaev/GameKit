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
	[Inject] private PersistentDataProxy _persistentDataProxy;
	[Inject] private PersistentDataClipboardProxy _persistentDataClipboardProxy;

	private void Awake()
	{
		_copyToClipboardButton.onClick.AddListener(_persistentDataClipboardProxy.CopyPersistentDataToClipboard);
		_pasteFromClipboardButton.onClick.AddListener(_persistentDataClipboardProxy.PastePersistentDataFromClipboard);
	}

	private void Start()
	{
		var persistentData = _persistentDataProxy.data;
		_userIdText.text = persistentData.userId;
		_firstLaunchTimeText.text = ConvertTimestampToReadableString(persistentData.firstLaunchTimestamp);
		_launchCountText.text = persistentData.launchesCounter.ToString(CultureInfo.InvariantCulture);
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
