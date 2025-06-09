using TMPro;
using UnityEngine;

public class DebugValue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _valueText;

    public void SetTitleText(string value)
    {
        _titleText.text = value;
    }

    public void SetValueText(string value)
    {
        _valueText.text = value;
    }
}
