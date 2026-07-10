using GameKit.Commands.Contracts;
using GameKit.ProductionMode.Contracts;
using GameKit.UiPages.Contracts;
using GameKit.UiRegions.Contracts;
using GameKit.UiRegionsControl.Contracts;
using JetBrains.Annotations;

namespace GameKit.Commands
{
    [UsedImplicitly]
    public class ShowInitialUiCommand : IShowInitialUiCommand
    {
        private readonly IUiRegionHostPresenter m_uiRegionHostPresenter;
        private readonly IPageNavigator m_pageNavigator;
        private readonly IProductionModeProvider m_productionModeProvider;

        public ShowInitialUiCommand(
            IUiRegionHostPresenter uiRegionHostPresenter,
            IPageNavigator pageNavigator,
            IProductionModeProvider productionModeProvider)
        {
            m_uiRegionHostPresenter = uiRegionHostPresenter;
            m_pageNavigator = pageNavigator;
            m_productionModeProvider = productionModeProvider;
        }

        public void Execute()
        {
            m_uiRegionHostPresenter.OnRegionElementShowing(UiRegionElementAddressableIds.k_TopPanel, UiRegionId.TopPanel);
            m_pageNavigator.ShowPage(UiRegionElementAddressableIds.k_MetaPage);

            var isDebug = !m_productionModeProvider.IsProduction;
            m_uiRegionHostPresenter.OnRegionActivating(UiRegionId.DebugPanelToolBar, isDebug);
            m_uiRegionHostPresenter.OnRegionActivating(UiRegionId.DebugPanelPage, isDebug);
            m_uiRegionHostPresenter.OnRegionActivating(UiRegionId.DebugPanelMessage, isDebug);
#if !IS_PRODUCTION
            m_uiRegionHostPresenter.OnRegionElementShowing(UiRegionElementAddressableIds.k_DebugToolBar, UiRegionId.DebugPanelToolBar);
#endif
        }
    }
}
