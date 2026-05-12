using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameKit.DebugPanelTabBar
{
    [ExecuteAlways]
    public class DebugPanelTabBarLogsIndicator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image indicatorImage;
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color warningColor;
        [SerializeField] private Color errorColor;
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
            switch (state)
            {
                case DebugPanelTabBarLogsIndicatorState.Default:
                    indicatorImage.color = defaultColor;
                    break;
                case DebugPanelTabBarLogsIndicatorState.Warning:
                    indicatorImage.color = warningColor;
                    break;
                case DebugPanelTabBarLogsIndicatorState.Error:
                    indicatorImage.color = errorColor;
                    break;
            }
        }
    }
}
