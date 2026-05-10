using GameKit.UiPopups.Contracts;
using GameKit.UiRegionsControl.Contracts;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.TopPanel
{
    [UsedImplicitly]
    public class TopPanelPresenter
    {
        [Inject] private IPopupNavigator m_popupNavigator;

        public void OpenSettingsPopup()
        {
            m_popupNavigator.Open(UiRegionElementAddressableIds.k_SettingsPopup);
        }
    }
}
