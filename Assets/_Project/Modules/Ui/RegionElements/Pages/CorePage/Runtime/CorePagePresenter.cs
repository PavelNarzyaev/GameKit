using GameKit.UiPages.Contracts;
using GameKit.UiBackgrounds.Contracts;
using GameKit.UiRegionsControl.Contracts;
using JetBrains.Annotations;

namespace GameKit.CorePage
{
    [UsedImplicitly]
    public class CorePagePresenter
    {
        private readonly IBackgroundNavigator m_backgroundNavigator;
        private readonly IPageNavigator m_pageNavigator;

        public CorePagePresenter(IBackgroundNavigator backgroundNavigator, IPageNavigator pageNavigator)
        {
            m_backgroundNavigator = backgroundNavigator;
            m_pageNavigator = pageNavigator;
        }

        public void ShowBackground()
        {
            m_backgroundNavigator.ShowBackground(UiRegionElementAddressableIds.k_CorePageBackground);
        }

        public void OpenMetaPage()
        {
            m_pageNavigator.ShowPage(UiRegionElementAddressableIds.k_MetaPage);
        }
    }
}
