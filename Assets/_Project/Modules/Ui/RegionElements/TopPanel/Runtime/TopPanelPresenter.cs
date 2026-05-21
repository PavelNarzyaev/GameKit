using GameKit.UiPopups.Contracts;
using GameKit.UiRegionsControl.Contracts;
using JetBrains.Annotations;

namespace GameKit.TopPanel
{
    [UsedImplicitly]
    public class TopPanelPresenter
    {
        private readonly IPopupNavigator m_popupNavigator;

        public TopPanelPresenter(IPopupNavigator popupNavigator)
        {
            m_popupNavigator = popupNavigator;
        }

        public void OpenSettingsPopup()
        {
            m_popupNavigator.Open(UiRegionElementAddressableIds.k_SettingsPopup);
        }
    }
}
