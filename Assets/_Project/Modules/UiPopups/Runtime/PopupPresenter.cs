using System;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.UiPopups
{
    [UsedImplicitly]
    public class PopupPresenter
    {
        [Inject] private PopupNavigator m_popupNavigator;

        public event Action FrontPopupChanged
        {
            add => m_popupNavigator.FrontPopupChanged += value;
            remove => m_popupNavigator.FrontPopupChanged -= value;
        }

        public bool IsFrontPopup(string addressableId)
        {
            return m_popupNavigator.FrontPopupAddressableId == addressableId;
        }

        public void SetFrontPopupModal(bool isModal)
        {
            m_popupNavigator.IsFrontPopupModal = isModal;
        }
    }
}
