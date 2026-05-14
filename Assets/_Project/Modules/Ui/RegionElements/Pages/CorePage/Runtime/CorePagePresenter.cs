using GameKit.UiPages.Contracts;
using GameKit.UiBackgrounds.Contracts;
using GameKit.UiRegionsControl.Contracts;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.CorePage
{
    [UsedImplicitly]
    public class CorePagePresenter
    {
        [Inject] private IBackgroundNavigator m_backgroundNavigator;
        [Inject] private IPageNavigator m_pageNavigator;

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
