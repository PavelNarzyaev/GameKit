using Proxies;
using JetBrains.Annotations;
using Zenject;

namespace Commands
{
    [UsedImplicitly]
    public class LaunchCommand
    {
        [Inject] private InitializeStateCommand _initializeStateCommand;
        [Inject] private LocalStateProxy _localStateProxy;
        [Inject] private ResetUiCommand _resetUiCommand;

        public void Execute()
        {
            bool isFirstLaunch = !_localStateProxy.CheckIfExists();
            if (isFirstLaunch)
            {
                _initializeStateCommand.Execute();
            }
            else
            {
                _localStateProxy.Refresh();
            }

            _localStateProxy.data.launchesCounter++;
            _localStateProxy.MarkAsDirty();

            _resetUiCommand.Execute();
        }
    }
}
