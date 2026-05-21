using GameKit.UiBackgrounds.Contracts;
using GameKit.UiRegions.Contracts;
using JetBrains.Annotations;

namespace GameKit.UiBackgrounds
{
    [UsedImplicitly]
    public class BackgroundNavigator : IBackgroundNavigator
    {
        private readonly IUiRegionHostPresenter m_uiRegionHostPresenter;

        private string m_currentBackgroundAddressableId;

        public BackgroundNavigator(IUiRegionHostPresenter uiRegionHostPresenter)
        {
            m_uiRegionHostPresenter = uiRegionHostPresenter;
        }

        public void ShowBackground(string addressableId)
        {
            if (m_currentBackgroundAddressableId == addressableId)
            {
                return;
            }

            if (m_currentBackgroundAddressableId != null)
            {
                m_uiRegionHostPresenter.OnRegionElementHidingIfExists(m_currentBackgroundAddressableId);
            }

            m_currentBackgroundAddressableId = addressableId;
            m_uiRegionHostPresenter.OnRegionElementShowing(addressableId, UiRegionId.Background);
        }

        public void Reset()
        {
            m_currentBackgroundAddressableId = null;
        }
    }
}
