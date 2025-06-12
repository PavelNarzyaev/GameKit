using JetBrains.Annotations;
using Proxies;
using Zenject;

namespace Commands
{
    [UsedImplicitly]
    public class LaunchCommand
    {
        [Inject] private InitializeStateCommand m_initializeStateCommand;
        [Inject] private LocalStateProxy m_localStateProxy;
        [Inject] private ResetUiCommand m_resetUiCommand;

        public void Execute()
        {
            var isFirstLaunch = !m_localStateProxy.CheckIfExists();
            if (isFirstLaunch)
            {
                m_initializeStateCommand.Execute();
            }
            else
            {
                m_localStateProxy.Refresh();
            }

            m_localStateProxy.Data.launchesCounter++;
            m_localStateProxy.MarkAsDirty();

            m_resetUiCommand.Execute();
        }
    }
}
