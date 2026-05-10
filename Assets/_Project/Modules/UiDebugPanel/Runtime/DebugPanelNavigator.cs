using System;
using GameKit.UiRegions;
using GameKit.UiRegionsControl;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.UiDebugPanel
{
    [UsedImplicitly]
    public class DebugPanelNavigator : IDebugPanelNavigator
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
            else
            {
                m_uiRegionHostPresenter.OnRegionElementShowing(UiRegionElementAddressableIds.k_DebugPageBackdrop, UiRegionId.DebugPanelPage);
            }

            CurrentPageAddressableId = addressableId;
            m_uiRegionHostPresenter.OnRegionElementShowing(addressableId, UiRegionId.DebugPanelPage);
            PageChanged?.Invoke();
        }

        public void Close()
        {
            if (CurrentPageAddressableId == null)
            {
                return;
            }

            m_uiRegionHostPresenter.OnRegionElementHidingIfExists(CurrentPageAddressableId);
            Reset();
            PageChanged?.Invoke();
        }

        public void Reset()
        {
            CurrentPageAddressableId = null;
            m_uiRegionHostPresenter.OnRegionElementHidingIfExists(UiRegionElementAddressableIds.k_DebugPageBackdrop);
        }
    }
}
