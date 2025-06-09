using Zenject;

public class LaunchCommand
{
    [Inject] private InitializeStateCommand _initializeStateCommand;
    [Inject] private LocalStateProxy _localStateProxy;
    [Inject] private ResetUiCommand _resetUiCommand;

    public void Execute()
    {
        var isFirstLaunch = !_localStateProxy.CheckIfExists();
        if (isFirstLaunch)
            _initializeStateCommand.Execute();
        else
            _localStateProxy.Refresh();
        _localStateProxy.data.launchesCounter++;
        _localStateProxy.MarkAsDirty();

        _resetUiCommand.Execute();
    }
}
