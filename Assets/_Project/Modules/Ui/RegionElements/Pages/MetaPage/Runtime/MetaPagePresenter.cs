using GameKit.UiPages.Contracts;
using GameKit.UiRegionsControl.Contracts;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.MetaPage
{
    [UsedImplicitly]
    public class MetaPagePresenter
    {
        [Inject] private IPageNavigator m_pageNavigator;

        public void OpenCorePage()
        {
            m_pageNavigator.ShowPage(UiRegionElementAddressableIds.k_CorePage);
        }
    }
}
