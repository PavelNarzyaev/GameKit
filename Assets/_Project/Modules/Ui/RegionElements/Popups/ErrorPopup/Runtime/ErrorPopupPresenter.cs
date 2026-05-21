using GameKit.Commands.Contracts;
using JetBrains.Annotations;

namespace GameKit.ErrorPopup
{
    [UsedImplicitly]
    public class ErrorPopupPresenter
    {
        private readonly IDestroyUiCommand m_destroyUiCommand;
        private readonly ILaunchCommand m_launchCommand;

        public ErrorPopupPresenter(IDestroyUiCommand destroyUiCommand, ILaunchCommand launchCommand)
        {
            m_destroyUiCommand = destroyUiCommand;
            m_launchCommand = launchCommand;
        }

        public void Reload()
        {
            m_destroyUiCommand.Execute();
            m_launchCommand.Execute();
        }
    }
}
