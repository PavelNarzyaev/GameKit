using GameKit.UiPages;
using GameKit.UiPopups;
using GameKit.UiRegions;
using JetBrains.Annotations;
using Zenject;
#if !IS_PRODUCTION
using GameKit.UiDebugPanel;
#endif

namespace GameKit.Commands
{
    [UsedImplicitly]
    public class DestroyUiCommand
    {
        [Inject] private UiRegionHostPresenter m_uiRegionHostPresenter;
        [Inject] private PageNavigator m_pageNavigator;
        [Inject] private PopupNavigator m_popupNavigator;
#if !IS_PRODUCTION
        [Inject] private DebugPanelNavigator m_debugPanelNavigator;
#endif

        public void Execute()
        {
            m_uiRegionHostPresenter.OnAllRegionElementsDestroying();
            m_popupNavigator.Reset();
            m_pageNavigator.Reset();
#if !IS_PRODUCTION
            m_debugPanelNavigator.Reset();
#endif
        }
    }
}
