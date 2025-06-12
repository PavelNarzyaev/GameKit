using Shapes2D;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MonoBehaviours
{
    public class NavigationPanelToggleButton : MonoBehaviour
    {
        [FormerlySerializedAs("_backgroundImage")] [SerializeField] private Shape backgroundImage;
        [FormerlySerializedAs("_text")] [SerializeField] private TextMeshProUGUI text;
        [FormerlySerializedAs("_button")] [SerializeField] private Button button;
        [FormerlySerializedAs("_selectedColor")] [SerializeField] private Color selectedColor;
        [FormerlySerializedAs("_defaultColor")] [SerializeField] private Color defaultColor;

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
