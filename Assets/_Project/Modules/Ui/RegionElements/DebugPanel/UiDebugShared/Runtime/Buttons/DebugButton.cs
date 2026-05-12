using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameKit.UiDebugShared
{
    public class DebugButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text text;
        [SerializeField] private Image background;

        private static readonly Color s_disabledColor = new(0.6156863f, 0.6156863f, 0.6156863f, 1f);
        private static readonly Color s_pressedColor = new(0.43529412f, 0.30980393f, 0.08627451f, 1f);
        private static readonly Color s_enabledColor = new(0.7411765f, 0.52156866f, 0.13333334f, 1f);

        private bool m_isEnabled = true;
        private bool m_isPressed;

        public void SetEnabled(bool isEnabled)
        {
            m_isEnabled = isEnabled;
            m_isPressed = false;

            button.interactable = isEnabled;

            RefreshDesign();
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

        private void RefreshDesign()
        {
            var color = !m_isEnabled
                ? s_disabledColor
                : m_isPressed
                    ? s_pressedColor
                    : s_enabledColor;

            text.color = color;
            background.color = color;
        }

        public void AddClickListener(UnityAction call)
        {
            button.onClick.AddListener(call);
        }
    }
}
