using Shapes2D;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MonoBehaviours
{
    public class NavigationPanelToggleButton : MonoBehaviour
    {
        [SerializeField] private Shape _backgroundImage;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Button _button;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _defaultColor;

        public void SetSelected(bool value)
        {
            _backgroundImage.settings.outlineColor = value ? _selectedColor : _defaultColor;
            _text.color = value ? _selectedColor : _defaultColor;
            _button.enabled = !value;
        }

        public void AddClickListener(UnityAction call)
        {
            _button.onClick.AddListener(call);
        }
    }
}
