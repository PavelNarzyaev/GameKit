using System;
using GameKit.UiBackgrounds.Contracts;
using GameKit.UiRegions.Contracts;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.UiBackgrounds
{
    [UsedImplicitly]
    public class BackgroundNavigator : IBackgroundNavigator
    {
        [Inject] private IUiRegionHostPresenter m_uiRegionHostPresenter;

        public string CurrentBackgroundAddressableId { get; private set; }

        public event Action BackgroundChanged;

        public void ShowBackground(string addressableId)
        {
            if (CurrentBackgroundAddressableId == addressableId)
            {
                return;
            }

            if (CurrentBackgroundAddressableId != null)
            {
                m_uiRegionHostPresenter.OnRegionElementHidingIfExists(CurrentBackgroundAddressableId);
            }

            CurrentBackgroundAddressableId = addressableId;
            m_uiRegionHostPresenter.OnRegionElementShowing(addressableId, UiRegionId.Background);
            BackgroundChanged?.Invoke();
        }

        public void Reset()
        {
            CurrentBackgroundAddressableId = null;
        }
    }
}
