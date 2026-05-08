using GameKit.UiPages;
using GameKit.UiRegionsControl;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.CorePage
{
    [UsedImplicitly]
    public class CorePagePresenter
    {
        [Inject] private PageNavigator m_pageNavigator;

        public void OpenMetaPage()
        {
            m_pageNavigator.ShowPage(UiRegionElementAddressableIds.k_MetaPage);
        }
    }
}
