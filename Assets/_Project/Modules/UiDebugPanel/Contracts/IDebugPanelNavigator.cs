using System;

namespace GameKit.UiDebugPanel
{
    public interface IDebugPanelNavigator
    {
        string CurrentPageAddressableId { get; }
        event Action PageChanged;

        void ShowPage(string addressableId);
        void Close();
        void Reset();
    }
}
