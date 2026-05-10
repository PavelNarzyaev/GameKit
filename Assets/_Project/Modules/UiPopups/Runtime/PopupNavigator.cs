using System;
using System.Collections.Generic;
using System.Linq;
using GameKit.UiRegionsControl;
using GameKit.UiRegions;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.UiPopups
{
    [UsedImplicitly]
    public class PopupNavigator : IPopupNavigator
    {
        private readonly List<string> m_stack = new();
        public bool IsFrontPopupModal { get; set; }
        public string FrontPopupAddressableId => m_stack.Count == 0 ? null : m_stack.Last();
        [Inject] private IUiRegionHostPresenter m_uiRegionHostPresenter;
        public event Action FrontPopupChanged;

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

        public void Reset()
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
