using GameKit.UiPopups.Contracts;
using JetBrains.Annotations;

namespace GameKit.UiPopups
{
    [UsedImplicitly]
    public class PopupBackdropPresenter
    {
        private readonly IPopupNavigator m_popupNavigator;

        public PopupBackdropPresenter(IPopupNavigator popupNavigator)
        {
            m_popupNavigator = popupNavigator;
        }

        public bool CanCloseFrontPopup()
        {
            return !m_popupNavigator.IsFrontPopupModal;
        }

        public void CloseFrontPopup()
        {
            m_popupNavigator.CloseFront();
        }
    }
}
