using GameKit.PlayerState;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace GameKit.StateClipboardProxy
{
    [UsedImplicitly]
    public class StateClipboardProxy
    {
        [Inject] private PlayerStateProvider m_playerStateProvider;

        public void CopyStateToClipboard()
        {
            UniClipboard.SetText(m_playerStateProvider.Get());
            Debug.Log("User state is copied to clipboard");
        }

        public void PasteStateFromClipboard()
        {
            m_playerStateProvider.Set(UniClipboard.GetText());
            Debug.Log("User state is applied from clipboard");
        }
    }
}
