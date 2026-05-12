using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameKit.DebugPanelTabBar
{
    [ExecuteAlways]
    public class DebugPanelTabBarLogsIndicator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private GameObject logIndicator;
        [SerializeField] private GameObject warningIndicator;
        [SerializeField] private GameObject errorIndicator;
        [SerializeField] private GameObject pressedIndicator;
        [SerializeField] private Button button;

        private bool m_isPressed;

        private void Awake()
        {
            pressedIndicator.SetActive(false);
        }

        public void AddClickListener(UnityAction call)
        {
            button.onClick.AddListener(call);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            pressedIndicator.SetActive(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            pressedIndicator.SetActive(false);
        }

        public void SetState(DebugPanelTabBarLogsIndicatorState state)
        {
            logIndicator.SetActive(state == DebugPanelTabBarLogsIndicatorState.Default);
            warningIndicator.SetActive(state == DebugPanelTabBarLogsIndicatorState.Warning);
            errorIndicator.SetActive(state == DebugPanelTabBarLogsIndicatorState.Error);
        }
    }
}
