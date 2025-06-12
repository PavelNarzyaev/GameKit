using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Proxies
{
    [UsedImplicitly]
    public class StateClipboardProxy
    {
        [Inject] private LocalStateProxy m_localStateProxy;

        public void CopyStateToClipboard()
        {
            UniClipboard.SetText(LocalStateProxy.Get());
            Debug.Log("User state is copied to clipboard");
        }

        public void PasteStateFromClipboard()
        {
            m_localStateProxy.Set(UniClipboard.GetText());
            Debug.Log("User state is applied from clipboard");
        }
    }
}
