using Zenject;

namespace GameKit
{

	public class ResetUiController
	{
		[Inject] private LocalStateProxy _localStateProxy;
		[Inject] private ResetUiCommand _resetUiCommand;

		[Inject]
		private void Inject()
		{
			_localStateProxy.refreshFromJsonEvent += _resetUiCommand.Execute;
		}
	}
}
