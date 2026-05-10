using JetBrains.Annotations;
using Zenject;

namespace GameKit.UiPopups
{
    [UsedImplicitly]
    public class PopupBackdropPresenter
    {
        [Inject] private IPopupNavigator m_popupNavigator;

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
