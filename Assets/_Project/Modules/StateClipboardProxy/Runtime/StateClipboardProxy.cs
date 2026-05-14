using GameKit.PlayerState.Contracts;
using GameKit.StateClipboardProxy.Contracts;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace GameKit.StateClipboardProxy
{
    [UsedImplicitly]
    public class StateClipboardProxy : IStateClipboardProxy
    {
        [Inject] private IPlayerStateProvider m_playerStateProvider;

        public void CopyStateToClipboard()
        {
            UniClipboard.SetText(m_playerStateProvider.ExportJson());
            Debug.Log("User state is copied to clipboard");
        }

        public void PasteStateFromClipboard()
        {
            m_playerStateProvider.ReplaceFromJson(UniClipboard.GetText());
            Debug.Log("User state is applied from clipboard");
        }
    }
}
