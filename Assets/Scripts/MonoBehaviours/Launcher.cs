using UnityEngine;
using Zenject;

public class Launcher : MonoBehaviour
{
	[Inject] private LaunchCommand _launchCommand;

	private void Start()
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
