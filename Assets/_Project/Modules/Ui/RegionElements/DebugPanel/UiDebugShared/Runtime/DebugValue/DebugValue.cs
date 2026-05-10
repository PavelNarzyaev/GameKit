using TMPro;
using UnityEngine;

namespace GameKit.UiDebugShared
{
    public class DebugValue : MonoBehaviour
    {
        [SerializeField] private TMP_Text valueText;

        public void SetValueText(string value)
        {
            valueText.text = value;
        }
    }
}
