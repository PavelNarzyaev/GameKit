using GameKit.UiDebugShared;
using UnityEngine;
using Zenject;

namespace GameKit.DebugToolBar
{
    public class DebugToolBarCloseButtonView : MonoBehaviour
    {
        [SerializeField] private DebugButton closeButton;

        [Inject] private DebugToolBarCloseButtonPresenter m_presenter;

        private void Awake()
        {
            closeButton.AddClickListener(m_presenter.Close);
        }

        private void OnEnable()
        {
            Refresh();
            m_presenter.StateChanged += HandleStateChanged;
        }

        private void OnDisable()
        {
            m_presenter.StateChanged -= HandleStateChanged;
        }

        private void HandleStateChanged()
        {
            Refresh();
        }

        private void Refresh()
        {
            closeButton.SetEnabled(m_presenter.IsInteractable);
        }
    }
}
