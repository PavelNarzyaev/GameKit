using GameKit.UiPopups.Contracts;
using GameKit.UiRegions;
using UnityEngine;
using Zenject;

namespace GameKit.UiPopups
{
    public class PopupView : UiRegionElement
    {
        [SerializeField] private bool isModal;
        [Inject] private IPopupPresenter m_presenter;

        private void OnEnable()
        {
            m_presenter.FrontPopupChanged += HandleFrontPopupChanged;
        }

        private void OnDisable()
        {
            m_presenter.FrontPopupChanged -= HandleFrontPopupChanged;
        }

        private void HandleFrontPopupChanged()
        {
            if (!m_presenter.IsFrontPopup(AddressableId))
            {
                return;
            }

            m_presenter.SetFrontPopupModal(isModal);
        }
    }
}
