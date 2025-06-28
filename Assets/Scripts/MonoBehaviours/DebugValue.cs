using TMPro;
using UnityEngine;

namespace MonoBehaviours
{
    public class DebugValue : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI valueText;

        public void SetValueText(string value)
        {
            valueText.text = value;
        }
    }
}
