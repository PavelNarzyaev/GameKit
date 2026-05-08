using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameKit.UiDebugShared
{
    [ExecuteAlways]
    public class DebugButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color pressedColor;
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text text;
        [SerializeField] private Image background;

        private bool m_isPressed;

        private void OnValidate()
        {
            if (Application.isPlaying)
            {
                return;
            }

            m_isPressed = false;
            RefreshDesign();
        }

        public void AddClickListener(UnityAction call)
        {
            button.onClick.AddListener(call);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_isPressed = true;
            RefreshDesign();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            m_isPressed = false;
            RefreshDesign();
        }

        private void RefreshDesign()
        {
            var color = m_isPressed ? pressedColor : defaultColor;

            if (background != null)
            {
                background.color = color;
            }

            if (text != null)
            {
                text.color = color;
            }
        }
    }
}
