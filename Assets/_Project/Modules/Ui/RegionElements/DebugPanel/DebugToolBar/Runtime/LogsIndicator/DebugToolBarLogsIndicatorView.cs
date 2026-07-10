using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace GameKit.DebugToolBar
{
    public class DebugToolBarLogsIndicatorView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private GameObject logIndicator;
        [SerializeField] private GameObject warningIndicator;
        [SerializeField] private GameObject errorIndicator;
        [SerializeField] private GameObject pressedIndicator;
        [SerializeField] private Button button;

        [Inject] private DebugToolBarLogsIndicatorPresenter m_presenter;

        private void Awake()
        {
            pressedIndicator.SetActive(false);
            button.onClick.AddListener(HandleClicked);
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

        private void HandleClicked()
        {
            m_presenter.ShowLogsPage();
        }

        private void HandleStateChanged()
        {
            Refresh();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            pressedIndicator.SetActive(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            pressedIndicator.SetActive(false);
        }

        private void Refresh()
        {
            var state = m_presenter.State;
            logIndicator.SetActive(state == DebugToolBarLogsIndicatorState.Default);
            warningIndicator.SetActive(state == DebugToolBarLogsIndicatorState.Warning);
            errorIndicator.SetActive(state == DebugToolBarLogsIndicatorState.Error);
        }
    }
}
