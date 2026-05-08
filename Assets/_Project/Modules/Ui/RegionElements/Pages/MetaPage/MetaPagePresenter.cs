using GameKit.UiPages;
using GameKit.UiRegionsControl;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.MetaPage
{
    [UsedImplicitly]
    public class MetaPagePresenter
    {
        [Inject] private PageNavigator m_pageNavigator;

        public void OpenCorePage()
        {
            m_pageNavigator.ShowPage(UiRegionElementAddressableIds.k_CorePage);
        }
    }
}
