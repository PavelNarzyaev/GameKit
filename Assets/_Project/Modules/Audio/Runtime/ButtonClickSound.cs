using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameKit.Audio
{
    [RequireComponent(typeof(Button))]
    public class ButtonClickSound : MonoBehaviour
    {
        private ButtonClickSoundPlayer m_buttonClickSoundPlayer;
        private Button m_button;

        [Inject]
        public void Construct(ButtonClickSoundPlayer buttonClickSoundPlayer)
        {
            m_buttonClickSoundPlayer = buttonClickSoundPlayer;
        }

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

        private void HandleButtonClicked()
        {
            m_buttonClickSoundPlayer.Play();
        }
    }
}
