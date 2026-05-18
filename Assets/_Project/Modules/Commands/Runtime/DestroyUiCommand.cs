using GameKit.Commands.Contracts;
using GameKit.UiBackgrounds.Contracts;
using GameKit.UiPages.Contracts;
using GameKit.UiPopups.Contracts;
using GameKit.UiRegions.Contracts;
using JetBrains.Annotations;
using Zenject;
#if !IS_PRODUCTION
using GameKit.UiDebugPanel.Contracts;
#endif

namespace GameKit.Commands
{
    [UsedImplicitly]
    public class DestroyUiCommand : IDestroyUiCommand
    {
        [Inject] private IBackgroundNavigator m_backgroundNavigator;
        [Inject] private IUiRegionHostPresenter m_uiRegionHostPresenter;
        [Inject] private IPageNavigator m_pageNavigator;
        [Inject] private IPopupNavigator m_popupNavigator;
#if !IS_PRODUCTION
        [Inject] private IDebugPanelPageNavigator m_debugPanelPageNavigator;
#endif

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
