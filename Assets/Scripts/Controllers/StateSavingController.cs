using Zenject;

public class StateSavingController : ITickable
{
	[Inject] private StateProxy _stateProxy;

	private string _filePath;

	public void Tick()
	{
		if (_stateProxy.isDirty)
			_stateProxy.RefreshFile();
	}
}
