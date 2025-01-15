using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Launcher : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _userIdText;
	[SerializeField] private TextMeshProUGUI _firstLaunchTimeText;
	[SerializeField] private TextMeshProUGUI _launchCountText;
	[SerializeField] private TextMeshProUGUI _currentTimeText;
	[SerializeField] private Button _copyToClipboardButton;
	[SerializeField] private Button _pasteFromClipboardButton;

	[Inject] private LaunchCommand _launchCommand;
	[Inject] private CurrentTimeProxy _currentTimeProxy;
	[Inject] private PersistentDataProxy _persistentDataProxy;
	[Inject] private PersistentDataClipboardProxy _persistentDataClipboardProxy;

	private void Awake()
	{
		_copyToClipboardButton.onClick.AddListener(CopyToClipboardButtonClickHandler);
		_pasteFromClipboardButton.onClick.AddListener(PasteFromClipboardButtonClickHandler);
	}

	private void CopyToClipboardButtonClickHandler()
	{
		_persistentDataClipboardProxy.CopyPersistentDataToClipboard();
	}

	private void PasteFromClipboardButtonClickHandler()
	{
		_persistentDataClipboardProxy.PastePersistentDataFromClipboard();
	}

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

		RefreshPersistentDataTexts();
	}

	private void Update()
	{
		RefreshCurrentTimeText();
	}

	private void OnEnable()
	{
		_persistentDataProxy.refreshFromJsonEvent += RefreshPersistentDataTexts;
	}

	private void OnDisable()
	{
		_persistentDataProxy.refreshFromJsonEvent -= RefreshPersistentDataTexts;
	}

	private void RefreshCurrentTimeText()
	{
		_currentTimeText.text = ConvertTimestampToReadableString(_currentTimeProxy.GetTimestamp());
	}

	private void RefreshPersistentDataTexts()
	{
		var persistentData = _persistentDataProxy.data;
		_userIdText.text = persistentData.userId;
		_firstLaunchTimeText.text = ConvertTimestampToReadableString(persistentData.firstLaunchTimestamp);
		_launchCountText.text = persistentData.launchesCounter.ToString(CultureInfo.InvariantCulture);
	}

	private string ConvertTimestampToReadableString(long value)
	{
		return value.ToLocalDateTime().ToString("yyyy-MM-dd HH:mm:ss");
	}
}
