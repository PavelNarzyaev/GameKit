using System;

namespace GameKit.UiPopups.Contracts
{
    public interface IPopupPresenter
    {
        event Action FrontPopupChanged;

        bool IsFrontPopup(string addressableId);
        void SetFrontPopupModal(bool isModal);
    }
}
