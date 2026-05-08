using GameKit.UiPopups;
using GameKit.UiRegionsControl;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.TopPanel
{
    [UsedImplicitly]
    public class TopPanelPresenter
    {
        [Inject] private PopupNavigator m_popupNavigator;

        public void OpenSettingsPopup()
        {
            m_popupNavigator.Open(UiRegionElementAddressableIds.k_SettingsPopup);
        }
    }
}
