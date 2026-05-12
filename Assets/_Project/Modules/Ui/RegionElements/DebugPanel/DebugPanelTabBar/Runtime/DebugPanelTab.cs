using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameKit.DebugPanelTabBar
{
    [ExecuteAlways]
    public class DebugPanelTab : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color pressedColor;
        [SerializeField] private Color defaultColor;
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text text;
        [SerializeField] private Image background;

        private bool m_isSelected;
        private bool m_isPressed;

        private void OnValidate()
        {
            if (Application.isPlaying)
            {
                return;
            }

            m_isSelected = false;
            m_isPressed = false;
            RefreshDesign();
        }

        public void SetSelected(bool isSelected)
        {
            m_isSelected = isSelected;
            m_isPressed = false;

            button.interactable = !isSelected;

            RefreshDesign();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (m_isSelected)
            {
                return;
            }

            m_isPressed = true;
            RefreshDesign();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (m_isSelected)
            {
                return;
            }

            m_isPressed = false;
            RefreshDesign();
        }

        private void RefreshDesign()
        {
            var color = m_isSelected
                ? selectedColor
                : m_isPressed
                    ? pressedColor
                    : defaultColor;

            if (text != null)
            {
                text.color = color;
            }

            if (background != null)
            {
                background.color = color;
            }
        }

        public void AddClickListener(UnityAction call)
        {
            button.onClick.AddListener(call);
        }
    }
}
