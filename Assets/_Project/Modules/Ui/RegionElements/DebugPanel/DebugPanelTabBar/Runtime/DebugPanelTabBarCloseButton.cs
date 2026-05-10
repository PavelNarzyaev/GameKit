using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameKit.DebugPanelTabBar
{
    [ExecuteAlways]
    public class DebugPanelTabBarCloseButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Color disabledColor;
        [SerializeField] private Color pressedColor;
        [SerializeField] private Color enabledColor;
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text text;
        [SerializeField] private Image background;

        private bool m_isEnabled;
        private bool m_isPressed;

        private void OnValidate()
        {
            if (Application.isPlaying)
            {
                return;
            }

            m_isEnabled = false;
            m_isPressed = false;
            RefreshDesign();
        }

        public void AddClickListener(UnityAction call)
        {
            button.onClick.AddListener(call);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!m_isEnabled)
            {
                return;
            }

            m_isPressed = true;
            RefreshDesign();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!m_isEnabled)
            {
                return;
            }

            m_isPressed = false;
            RefreshDesign();
        }

        public void SetEnabled(bool isEnabled)
        {
            m_isEnabled = isEnabled;
            m_isPressed = false;

            button.enabled = isEnabled;
            RefreshDesign();
        }

        private void RefreshDesign()
        {
            var color = !m_isEnabled ? disabledColor : m_isPressed ? pressedColor : enabledColor;

            if (text != null)
            {
                text.color = color;
            }

            if (background != null)
            {
                background.color = color;
            }
        }
    }
}
