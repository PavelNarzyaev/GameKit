using System;
using GameKit.UiBackgrounds.Contracts;
using GameKit.UiRegions.Contracts;
using GameKit.UiReset.Contracts;
using JetBrains.Annotations;

namespace GameKit.UiBackgrounds
{
    [UsedImplicitly]
    public class BackgroundNavigator : IBackgroundNavigator, IDisposable
    {
        private readonly IUiRegionHostPresenter m_uiRegionHostPresenter;
        private readonly IUiResetEventListener m_uiResetEventListener;

        private string m_currentBackgroundAddressableId;

        public BackgroundNavigator(
            IUiRegionHostPresenter uiRegionHostPresenter,
            IUiResetEventListener uiResetEventListener)
        {
            m_uiRegionHostPresenter = uiRegionHostPresenter;
            m_uiResetEventListener = uiResetEventListener;
            m_uiResetEventListener.ResetRequested += HandleUiResetRequested;
        }

        public void Dispose()
        {
            m_uiResetEventListener.ResetRequested -= HandleUiResetRequested;
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

        private void HandleUiResetRequested()
        {
            m_currentBackgroundAddressableId = null;
        }
    }
}
