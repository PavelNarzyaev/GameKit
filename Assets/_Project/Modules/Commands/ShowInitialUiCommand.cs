using GameKit.ProductionMode;
using GameKit.UiPages;
using GameKit.UiRegions;
using GameKit.UiRegionsControl;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.Commands
{
    [UsedImplicitly]
    public class ShowInitialUiCommand
    {
        [Inject] private UiRegionHostPresenter m_uiRegionHostPresenter;
        [Inject] private PageNavigator m_pageNavigator;
        [Inject] private ProductionModeProvider m_productionModeProvider;

        public void Execute()
        {
            m_uiRegionHostPresenter.OnRegionElementShowing(UiRegionElementAddressableIds.k_TopPanel, UiRegionId.TopPanel);
            m_pageNavigator.ShowPage(UiRegionElementAddressableIds.k_MetaPage);

            var isDebug = !m_productionModeProvider.IsProduction;
            m_uiRegionHostPresenter.OnRegionActivating(UiRegionId.DebugPanelTabBar, isDebug);
            m_uiRegionHostPresenter.OnRegionActivating(UiRegionId.DebugPanelPage, isDebug);
#if !IS_PRODUCTION
            m_uiRegionHostPresenter.OnRegionElementShowing(UiRegionElementAddressableIds.k_DebugPanelTabBar, UiRegionId.DebugPanelTabBar);
#endif
        }
    }
}
