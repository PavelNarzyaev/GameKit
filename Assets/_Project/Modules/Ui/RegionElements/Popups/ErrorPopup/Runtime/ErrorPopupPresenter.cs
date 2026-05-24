using GameKit.Commands.Contracts;
using GameKit.UiReset.Contracts;
using JetBrains.Annotations;

namespace GameKit.ErrorPopup
{
    [UsedImplicitly]
    public class ErrorPopupPresenter
    {
        private readonly IUiResetEventPublisher m_uiResetEventPublisher;
        private readonly ILaunchCommand m_launchCommand;

        public ErrorPopupPresenter(IUiResetEventPublisher uiResetEventPublisher, ILaunchCommand launchCommand)
        {
            m_uiResetEventPublisher = uiResetEventPublisher;
            m_launchCommand = launchCommand;
        }

        public void Reload()
        {
            m_uiResetEventPublisher.PublishReset();
            m_launchCommand.Execute();
        }
    }
}
