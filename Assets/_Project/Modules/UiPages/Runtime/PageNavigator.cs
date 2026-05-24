using System;
using GameKit.UiPages.Contracts;
using GameKit.UiRegions.Contracts;
using GameKit.UiReset.Contracts;
using JetBrains.Annotations;

namespace GameKit.UiPages
{
    [UsedImplicitly]
    public class PageNavigator : IPageNavigator, IDisposable
    {
        private readonly IUiRegionHostPresenter m_uiRegionHostPresenter;
        private readonly IUiResetEventListener m_uiResetEventListener;

        private string m_currentPageAddressableId;

        public PageNavigator(
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

        private void HandleUiResetRequested()
        {
            m_currentPageAddressableId = null;
        }
    }
}
