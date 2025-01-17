using Zenject;

public class ResetUiController
{
	[Inject] private StateProxy _stateProxy;
	[Inject] private ResetUiCommand _resetUiCommand;

	[Inject]
	private void Inject()
	{
		_stateProxy.refreshFromJsonEvent += _resetUiCommand.Execute;
	}
}
