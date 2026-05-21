using GameKit.UiPages.Contracts;
using GameKit.UiBackgrounds.Contracts;
using GameKit.UiRegionsControl.Contracts;
using JetBrains.Annotations;

namespace GameKit.MetaPage
{
    [UsedImplicitly]
    public class MetaPagePresenter
    {
        private readonly IBackgroundNavigator m_backgroundNavigator;
        private readonly IPageNavigator m_pageNavigator;

        public MetaPagePresenter(IBackgroundNavigator backgroundNavigator, IPageNavigator pageNavigator)
        {
            m_backgroundNavigator = backgroundNavigator;
            m_pageNavigator = pageNavigator;
        }

        public void ShowBackground()
        {
            m_backgroundNavigator.ShowBackground(UiRegionElementAddressableIds.k_MetaPageBackground);
        }

        public void OpenCorePage()
        {
            m_pageNavigator.ShowPage(UiRegionElementAddressableIds.k_CorePage);
        }
    }
}
