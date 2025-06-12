using TMPro;
using UnityEngine;

namespace MonoBehaviours
{
    public class DebugValue : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI valueText;

        public void SetTitleText(string value)
        {
            titleText.text = value;
        }

        public void SetValueText(string value)
        {
            valueText.text = value;
        }
    }
}
