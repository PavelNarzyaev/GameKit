using UnityEngine;
using Zenject;

public class StateClipboardProxy
{
	[Inject] private StateProxy _stateProxy;

	public void CopyStateToClipboard()
	{
		UniClipboard.SetText(_stateProxy.LoadJsonFromFile());
		Debug.Log("User state is copied to clipboard");
	}

	public void PasteStateFromClipboard()
	{
		_stateProxy.RefreshFromJson(UniClipboard.GetText());
		Debug.Log("User state is applied from clipboard");
	}
}
