using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameKit.SettingsPopup
{
    [RequireComponent(typeof(Button))]
    public class Checkbox : MonoBehaviour
    {
        [SerializeField] private GameObject onState;
        [SerializeField] private GameObject offState;

        private Button m_button;
        private bool m_isOn;

        public event Action Clicked;

        private void Awake()
        {
            m_button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            m_button.onClick.AddListener(HandleButtonClicked);
        }

        private void OnDisable()
        {
            m_button.onClick.RemoveListener(HandleButtonClicked);
        }

        public void SetIsOn(bool isOn)
        {
            m_isOn = isOn;
            RefreshState();
        }

        private void HandleButtonClicked()
        {
            Clicked?.Invoke();
        }

        private void RefreshState()
        {
            onState.SetActive(m_isOn);
            offState.SetActive(!m_isOn);
        }
    }
}
