using Zenject;

public class StateSavingController : ITickable
{
	[Inject] private LocalStateProxy _localStateProxy;

	private string _filePath;

	public void Tick()
	{
		if (_localStateProxy.isDirty)
			_localStateProxy.Save();
	}
}
