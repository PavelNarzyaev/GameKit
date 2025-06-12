using Shapes2D;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MonoBehaviours
{
    public class NavigationPanelToggleButton : MonoBehaviour
    {
        [SerializeField] private Shape backgroundImage;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Button button;
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color defaultColor;

        public void SetSelected(bool value)
        {
            backgroundImage.settings.outlineColor = value ? selectedColor : defaultColor;
            text.color = value ? selectedColor : defaultColor;
            button.enabled = !value;
        }

        public void AddClickListener(UnityAction call)
        {
            button.onClick.AddListener(call);
        }
    }
}
