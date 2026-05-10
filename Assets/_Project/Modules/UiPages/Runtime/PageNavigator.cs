using System;
using GameKit.UiRegionsControl;
using GameKit.UiRegions;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.UiPages
{
    [UsedImplicitly]
    public class PageNavigator : IPageNavigator
    {
        [Inject] private IUiRegionHostPresenter m_uiRegionHostPresenter;
        public string CurrentPageAddressableId { get; private set; }

        public event Action PageChanged;

        public void ShowPage(string addressableId)
        {
            if (CurrentPageAddressableId == addressableId)
            {
                return;
            }

            if (CurrentPageAddressableId != null)
            {
                m_uiRegionHostPresenter.OnRegionElementHidingIfExists(CurrentPageAddressableId);
            }

            CurrentPageAddressableId = addressableId;
            m_uiRegionHostPresenter.OnRegionElementShowing(addressableId, UiRegionId.Page);
            PageChanged?.Invoke();
        }

        public void Reset()
        {
            CurrentPageAddressableId = null;
        }
    }
}
