using GameKit.Commands;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace GameKit.InputActionsProcessor
{
    public class InputActionsProcessor : MonoBehaviour
    {
        private CustomInputActions m_inputActions;
        [Inject] private ResetSceneCommand m_resetSceneCommand;

        private void Awake()
        {
            m_inputActions = new CustomInputActions();
        }

        private void OnEnable()
        {
            m_inputActions.utility.refresh.performed += HandleRefreshPerformed;
            m_inputActions.Enable();
        }

        private void OnDisable()
        {
            m_inputActions.utility.refresh.performed -= HandleRefreshPerformed;
            m_inputActions.Disable();
        }

        private void HandleRefreshPerformed(InputAction.CallbackContext context)
        {
            m_resetSceneCommand.Execute();
        }
    }
}
