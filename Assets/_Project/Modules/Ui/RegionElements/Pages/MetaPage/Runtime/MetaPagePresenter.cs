using GameKit.UiPages.Contracts;
using GameKit.UiBackgrounds.Contracts;
using GameKit.UiRegionsControl.Contracts;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.MetaPage
{
    [UsedImplicitly]
    public class MetaPagePresenter
    {
        [Inject] private IBackgroundNavigator m_backgroundNavigator;
        [Inject] private IPageNavigator m_pageNavigator;

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
