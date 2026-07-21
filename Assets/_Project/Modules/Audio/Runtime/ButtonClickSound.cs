using GameKit.Audio.Contracts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameKit.Audio
{
    [RequireComponent(typeof(Button))]
    public class ButtonClickSound : MonoBehaviour
    {
        private IAudioConfig m_audioConfig;
        private ISoundPlayer m_soundPlayer;
        private Button m_button;

        [Inject]
        public void Construct(IAudioConfig audioConfig, ISoundPlayer soundPlayer)
        {
            m_audioConfig = audioConfig;
            m_soundPlayer = soundPlayer;
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
            m_soundPlayer.Play(m_audioConfig.ButtonClick);
        }
    }
}
