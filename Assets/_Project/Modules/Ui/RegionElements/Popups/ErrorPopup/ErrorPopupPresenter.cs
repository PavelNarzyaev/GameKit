using GameKit.Commands;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.ErrorPopup
{
    [UsedImplicitly]
    public class ErrorPopupPresenter
    {
        [Inject] private DestroyUiCommand m_destroyUiCommand;
        [Inject] private LaunchCommand m_launchCommand;

        public void Reload()
        {
            m_destroyUiCommand.Execute();
            m_launchCommand.Execute();
        }
    }
}
