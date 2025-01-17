using UnityEngine;
using Zenject;

public class PersistentDataClipboardProxy
{
	[Inject] private PersistentDataProxy _persistentDataProxy;

	public void CopyPersistentDataToClipboard()
	{
		UniClipboard.SetText(_persistentDataProxy.LoadJsonFromFile());
		Debug.Log("Persistent data is copied to clipboard");
	}

	public void PastePersistentDataFromClipboard()
	{
		_persistentDataProxy.RefreshFromJson(UniClipboard.GetText());
		Debug.Log("Persistent data is applied from clipboard");
	}
}
