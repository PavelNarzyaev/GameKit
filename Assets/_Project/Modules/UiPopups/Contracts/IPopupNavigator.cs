using System;

namespace GameKit.UiPopups
{
    public interface IPopupNavigator
    {
        bool IsFrontPopupModal { get; set; }
        string FrontPopupAddressableId { get; }
        event Action FrontPopupChanged;

        void Open(string addressableId);
        void Close(string addressableId);
        void CloseFront();
        void Reset();
    }
}
