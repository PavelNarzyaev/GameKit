using UnityEngine;
using Zenject;

public class ResetUiController
{
	[Inject] private PersistentDataProxy _persistentDataProxy;
	[Inject] private ResetUiCommand _resetUiCommand;

	[Inject]
	private void Inject()
	{
		_persistentDataProxy.refreshFromJsonEvent += () => { Debug.Log("Reset controller"); };
		_persistentDataProxy.refreshFromJsonEvent += _resetUiCommand.Execute;
	}
}
