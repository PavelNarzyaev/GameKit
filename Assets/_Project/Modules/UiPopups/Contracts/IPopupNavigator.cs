using System;

namespace GameKit.UiPopups.Contracts
{
    public interface IPopupNavigator
    {
        bool IsFrontPopupModal { get; set; }
        string FrontPopupAddressableId { get; }
        event Action FrontPopupChanged;

        void Open(string addressableId);
        void Close(string addressableId);
        void CloseFront();
    }
}
