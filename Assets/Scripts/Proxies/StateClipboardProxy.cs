using UnityEngine;
using Zenject;

namespace Proxies
{
	public class StateClipboardProxy
	{
		[Inject] private LocalStateProxy _localStateProxy;

		public void CopyStateToClipboard()
		{
			UniClipboard.SetText(_localStateProxy.Get());
			Debug.Log("User state is copied to clipboard");
		}

		public void PasteStateFromClipboard()
		{
			_localStateProxy.Set(UniClipboard.GetText());
			Debug.Log("User state is applied from clipboard");
		}
	}
}
