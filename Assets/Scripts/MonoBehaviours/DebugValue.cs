using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace MonoBehaviours
{
    public class DebugValue : MonoBehaviour
    {
        [FormerlySerializedAs("_titleText")] [SerializeField] private TextMeshProUGUI titleText;
        [FormerlySerializedAs("_valueText")] [SerializeField] private TextMeshProUGUI valueText;

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
