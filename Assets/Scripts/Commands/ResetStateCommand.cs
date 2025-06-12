using JetBrains.Annotations;
using Proxies;
using Zenject;

namespace Commands
{
    [UsedImplicitly]
    public class ResetStateCommand
    {
        [Inject] private LaunchCommand m_launchCommand;

        public void Execute()
        {
            LocalStateProxy.Delete();
            m_launchCommand.Execute();
        }
    }
}
