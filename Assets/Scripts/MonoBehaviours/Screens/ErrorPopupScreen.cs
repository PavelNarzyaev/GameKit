using Commands;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MonoBehaviours.Screens
{
    public class ErrorPopupScreen : PopupScreenBase
    {
        [SerializeField] private Button reloadButton;
        [SerializeField] private Button deleteSaveButton;
        [Inject] private LaunchCommand m_launchCommand;
        [Inject] private ResetStateCommand m_resetStateCommand;

        private void Awake()
        {
            reloadButton.onClick.AddListener(m_launchCommand.Execute);
            deleteSaveButton.onClick.AddListener(m_resetStateCommand.Execute);
        }

        protected override bool IsModal()
        {
            return true;
        }
    }
}
