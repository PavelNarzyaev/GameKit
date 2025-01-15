using Zenject;

public class PersistentDataController : ITickable
{
	[Inject] private PersistentDataProxy _persistentDataProxy;

	private string _filePath;

	public void Tick()
	{
		if (_persistentDataProxy.isDirty)
			_persistentDataProxy.Save();
	}
}
