using System;
using GameKit.UiDebugPanel.Contracts;
using GameKit.UiRegions.Contracts;
using GameKit.UiRegionsControl.Contracts;
using GameKit.UiReset.Contracts;
using JetBrains.Annotations;

namespace GameKit.UiDebugPanel
{
    [UsedImplicitly]
    public class DebugPanelPageNavigator : IDebugPanelPageNavigator, IDisposable
    {
        private readonly IUiRegionHostPresenter m_uiRegionHostPresenter;
        private readonly IUiResetEventListener m_uiResetEventListener;
        public string CurrentPageAddressableId { get; private set; }

        public event Action PageChanged;

        public DebugPanelPageNavigator(
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

        public void Show(string addressableId)
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
            m_uiRegionHostPresenter.OnRegionElementHidingIfExists(UiRegionElementAddressableIds.k_DebugPageBackdrop);
            ResetCurrentPage();
            PageChanged?.Invoke();
        }

        private void HandleUiResetRequested()
        {
            ResetCurrentPage();
        }

        private void ResetCurrentPage()
        {
            CurrentPageAddressableId = null;
        }
    }
}
