using System;

namespace GameKit.UiPages
{
    public interface IPageNavigator
    {
        string CurrentPageAddressableId { get; }
        event Action PageChanged;

        void ShowPage(string addressableId);
        void Reset();
    }
}
