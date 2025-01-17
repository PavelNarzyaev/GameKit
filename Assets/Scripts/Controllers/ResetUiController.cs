using UnityEngine;
using Zenject;

public class ResetUiController
{
	[Inject] private StateProxy _stateProxy;
	[Inject] private ResetUiCommand _resetUiCommand;

	[Inject]
	private void Inject()
	{
		_stateProxy.refreshFromJsonEvent += () => { Debug.Log("Reset controller"); };
		_stateProxy.refreshFromJsonEvent += _resetUiCommand.Execute;
	}
}
