using GameKit.Commands.Contracts;
using GameKit.UiBackgrounds.Contracts;
using GameKit.UiPages.Contracts;
using GameKit.UiPopups.Contracts;
using GameKit.UiRegions.Contracts;
using JetBrains.Annotations;
#if !IS_PRODUCTION
using GameKit.UiDebugPanel.Contracts;
#endif

namespace GameKit.Commands
{
    [UsedImplicitly]
    public class DestroyUiCommand : IDestroyUiCommand
    {
        private readonly IBackgroundNavigator m_backgroundNavigator;
        private readonly IUiRegionHostPresenter m_uiRegionHostPresenter;
        private readonly IPageNavigator m_pageNavigator;
        private readonly IPopupNavigator m_popupNavigator;
#if !IS_PRODUCTION
        private readonly IDebugPanelPageNavigator m_debugPanelPageNavigator;
#endif

        public DestroyUiCommand(
#if !IS_PRODUCTION
            IDebugPanelPageNavigator debugPanelPageNavigator,
#endif
            IBackgroundNavigator backgroundNavigator,
            IUiRegionHostPresenter uiRegionHostPresenter,
            IPageNavigator pageNavigator,
            IPopupNavigator popupNavigator)
        {
#if !IS_PRODUCTION
            m_debugPanelPageNavigator = debugPanelPageNavigator;
#endif
            m_backgroundNavigator = backgroundNavigator;
            m_uiRegionHostPresenter = uiRegionHostPresenter;
            m_pageNavigator = pageNavigator;
            m_popupNavigator = popupNavigator;
        }

        public void Execute()
        {
            m_uiRegionHostPresenter.OnAllRegionElementsDestroying();
            m_backgroundNavigator.Reset();
            m_popupNavigator.Reset();
            m_pageNavigator.Reset();
#if !IS_PRODUCTION
            m_debugPanelPageNavigator.Reset();
#endif
        }
    }
}
