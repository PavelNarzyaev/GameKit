using Zenject;

public class PersistentDataClipboardProxy
{
	[Inject] private PersistentDataProxy _persistentDataProxy;

	public void CopyPersistentDataToClipboard()
	{
		UniClipboard.SetText(_persistentDataProxy.LoadJsonFromFile());
	}

	public void PastePersistentDataFromClipboard()
	{
		_persistentDataProxy.RefreshFromJson(UniClipboard.GetText());
	}
}
