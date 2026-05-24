using System;
using GameKit.UiRegions.Contracts;
using GameKit.UiReset.Contracts;
using JetBrains.Annotations;

namespace GameKit.UiRegions
{
    [UsedImplicitly]
    public class UiRegionHostPresenter : IUiRegionHostPresenter, IDisposable
    {
        private readonly IUiResetEventListener m_uiResetEventListener;

        public event Action<string, UiRegionId> RegionElementShowing;
        public event Action<string> RegionElementHidingIfExists;
        public event Action AllRegionElementsDestroying;
        public event Action<string, int> RegionElementIndexSetting;
        public event Action<UiRegionId, bool> RegionActivating;

        public UiRegionHostPresenter(IUiResetEventListener uiResetEventListener)
        {
            m_uiResetEventListener = uiResetEventListener;
            m_uiResetEventListener.ResetRequested += HandleUiResetRequested;
        }

        public void Dispose()
        {
            m_uiResetEventListener.ResetRequested -= HandleUiResetRequested;
        }

        public void OnRegionElementShowing(string addressableId, UiRegionId region)
        {
            RegionElementShowing?.Invoke(addressableId, region);
        }

        public void OnRegionElementHidingIfExists(string addressableId)
        {
            RegionElementHidingIfExists?.Invoke(addressableId);
        }

        public void OnAllRegionElementsDestroying()
        {
            AllRegionElementsDestroying?.Invoke();
        }

        public void OnRegionElementIndexSetting(string addressableId, int index)
        {
            RegionElementIndexSetting?.Invoke(addressableId, index);
        }

        public void OnRegionActivating(UiRegionId regionId, bool isActive)
        {
            RegionActivating?.Invoke(regionId, isActive);
        }

        private void HandleUiResetRequested()
        {
            AllRegionElementsDestroying?.Invoke();
        }
    }
}
