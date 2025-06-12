using Commands;
using JetBrains.Annotations;
using Zenject;

namespace Controllers
{
    [UsedImplicitly]
    public class EntryPointController : IInitializable
    {
        [Inject] private LaunchCommand m_launchCommand;

        public void Initialize()
        {
            // try
            // {
            m_launchCommand.Execute();
            // }
            // catch (Exception e)
            // {
            // TODO: show launch error popup
            // throw;
            // }
        }
    }
}
