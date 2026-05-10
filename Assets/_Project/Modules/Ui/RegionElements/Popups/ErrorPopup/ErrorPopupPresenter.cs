using GameKit.Commands;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.ErrorPopup
{
    [UsedImplicitly]
    public class ErrorPopupPresenter
    {
        [Inject] private IDestroyUiCommand m_destroyUiCommand;
        [Inject] private ILaunchCommand m_launchCommand;

        public void Reload()
        {
            m_destroyUiCommand.Execute();
            m_launchCommand.Execute();
        }
    }
}
