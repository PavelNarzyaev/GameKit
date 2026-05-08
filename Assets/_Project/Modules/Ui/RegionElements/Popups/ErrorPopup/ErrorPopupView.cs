using GameKit.UiPopups;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameKit.ErrorPopup
{
    public class ErrorPopupView : PopupView
    {
        [SerializeField] private Button reloadButton;
        [Inject] private ErrorPopupPresenter m_presenter;

        private void Awake()
        {
            reloadButton.onClick.AddListener(m_presenter.Reload);
        }
    }
}
