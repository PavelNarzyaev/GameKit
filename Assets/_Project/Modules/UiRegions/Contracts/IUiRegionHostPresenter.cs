using System;

namespace GameKit.UiRegions
{
    public interface IUiRegionHostPresenter
    {
        event Action<string, UiRegionId> RegionElementShowing;
        event Action<string> RegionElementHidingIfExists;
        event Action AllRegionElementsDestroying;
        event Action<string, int> RegionElementIndexSetting;
        event Action<UiRegionId, bool> RegionActivating;

        void OnRegionElementShowing(string addressableId, UiRegionId region);
        void OnRegionElementHidingIfExists(string addressableId);
        void OnAllRegionElementsDestroying();
        void OnRegionElementIndexSetting(string addressableId, int index);
        void OnRegionActivating(UiRegionId regionId, bool isActive);
    }
}
