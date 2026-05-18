using System;

namespace GameKit.UiDebugPanel.Contracts
{
    public interface IDebugPanelPageNavigator
    {
        string CurrentPageAddressableId { get; }
        event Action PageChanged;

        void Show(string addressableId);
        void Close();
        void Reset();
    }
}
