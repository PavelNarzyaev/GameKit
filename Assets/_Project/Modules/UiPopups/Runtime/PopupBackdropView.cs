using GameKit.UiRegions;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameKit.UiPopups
{
    public class PopupBackdropView : UiRegionElement
    {
        [SerializeField] private Button button;
        [Inject] private PopupBackdropPresenter m_presenter;

        private void OnEnable()
        {
            button.onClick.AddListener(HandleButtonClicked);
        }

        private void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }

        private void HandleButtonClicked()
        {
            if (m_presenter.CanCloseFrontPopup())
            {
                m_presenter.CloseFrontPopup();
            }
        }
    }
}
