using System;
using System.Collections.Generic;
using System.Linq;
using GameKit.UiPopups.Contracts;
using GameKit.UiRegions.Contracts;
using GameKit.UiRegionsControl.Contracts;
using GameKit.UiReset.Contracts;
using JetBrains.Annotations;

namespace GameKit.UiPopups
{
    [UsedImplicitly]
    public class PopupNavigator : IPopupNavigator, IDisposable
    {
        private readonly List<string> m_stack = new();
        private readonly IUiRegionHostPresenter m_uiRegionHostPresenter;
        private readonly IUiResetEventListener m_uiResetEventListener;
        public bool IsFrontPopupModal { get; set; }
        public string FrontPopupAddressableId => m_stack.Count == 0 ? null : m_stack.Last();
        public event Action FrontPopupChanged;

        public PopupNavigator(
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

        public void Open(string addressableId)
        {
            if (m_stack.Count == 0)
            {
                m_uiRegionHostPresenter.OnRegionElementShowing(UiRegionElementAddressableIds.k_PopupBackdrop, UiRegionId.Popups);
            }

            m_uiRegionHostPresenter.OnRegionElementShowing(addressableId, UiRegionId.Popups);
            m_stack.Add(addressableId);
            FrontPopupChanged?.Invoke();
            RefreshBackdropIndex();
        }

        public void Close(string addressableId)
        {
            m_uiRegionHostPresenter.OnRegionElementHidingIfExists(addressableId);

            if (!m_stack.Remove(addressableId))
            {
                return;
            }

            if (m_stack.Count == 0)
            {
                IsFrontPopupModal = false;
                m_uiRegionHostPresenter.OnRegionElementHidingIfExists(UiRegionElementAddressableIds.k_PopupBackdrop);
            }
            else
            {
                RefreshBackdropIndex();
                FrontPopupChanged?.Invoke();
            }
        }

        public void CloseFront()
        {
            if (m_stack.Count == 0)
            {
                return;
            }

            Close(m_stack.Last());
        }

        private void HandleUiResetRequested()
        {
            m_stack.Clear();
            IsFrontPopupModal = false;
        }

        private void RefreshBackdropIndex()
        {
            m_uiRegionHostPresenter.OnRegionElementIndexSetting(UiRegionElementAddressableIds.k_PopupBackdrop, m_stack.Count - 1);
        }
    }
}
