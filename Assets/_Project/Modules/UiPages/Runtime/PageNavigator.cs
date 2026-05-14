using GameKit.UiPages.Contracts;
using GameKit.UiRegions.Contracts;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.UiPages
{
    [UsedImplicitly]
    public class PageNavigator : IPageNavigator
    {
        [Inject] private IUiRegionHostPresenter m_uiRegionHostPresenter;

        private string m_currentPageAddressableId;

        public void ShowPage(string addressableId)
        {
            if (m_currentPageAddressableId == addressableId)
            {
                return;
            }

            if (m_currentPageAddressableId != null)
            {
                m_uiRegionHostPresenter.OnRegionElementHidingIfExists(m_currentPageAddressableId);
            }

            m_currentPageAddressableId = addressableId;
            m_uiRegionHostPresenter.OnRegionElementShowing(addressableId, UiRegionId.Page);
        }

        public void Reset()
        {
            m_currentPageAddressableId = null;
        }
    }
}
