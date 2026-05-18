using GameKit.Commands.Contracts;
using GameKit.ProductionMode.Contracts;
using GameKit.UiPages.Contracts;
using GameKit.UiRegions.Contracts;
using GameKit.UiRegionsControl.Contracts;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.Commands
{
    [UsedImplicitly]
    public class ShowInitialUiCommand : IShowInitialUiCommand
    {
        [Inject] private IUiRegionHostPresenter m_uiRegionHostPresenter;
        [Inject] private IPageNavigator m_pageNavigator;
        [Inject] private IProductionModeProvider m_productionModeProvider;

        public void Execute()
        {
            m_uiRegionHostPresenter.OnRegionElementShowing(UiRegionElementAddressableIds.k_TopPanel, UiRegionId.TopPanel);
            m_pageNavigator.ShowPage(UiRegionElementAddressableIds.k_MetaPage);

            var isDebug = !m_productionModeProvider.IsProduction;
            m_uiRegionHostPresenter.OnRegionActivating(UiRegionId.DebugPanelTabBar, isDebug);
            m_uiRegionHostPresenter.OnRegionActivating(UiRegionId.DebugPanelPage, isDebug);
            m_uiRegionHostPresenter.OnRegionActivating(UiRegionId.DebugPanelMessage, isDebug);
#if !IS_PRODUCTION
            m_uiRegionHostPresenter.OnRegionElementShowing(UiRegionElementAddressableIds.k_DebugPanelTabBar, UiRegionId.DebugPanelTabBar);
#endif
        }
    }
}
