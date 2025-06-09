using Zenject;

public class EntryPointController : IInitializable
{
    [Inject] private LaunchCommand _launchCommand;

    public void Initialize()
    {
        // try
        // {
        _launchCommand.Execute();
        // }
        // catch (Exception e)
        // {
        // TODO: show launch error popup
        // throw;
        // }
    }
}
