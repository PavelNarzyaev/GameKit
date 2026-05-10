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
    public class DestroyUiCommand : IDestroyUiCommand
    {
        [Inject] private IUiRegionHostPresenter m_uiRegionHostPresenter;
        [Inject] private IPageNavigator m_pageNavigator;
        [Inject] private IPopupNavigator m_popupNavigator;
#if !IS_PRODUCTION
        [Inject] private IDebugPanelNavigator m_debugPanelNavigator;
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
