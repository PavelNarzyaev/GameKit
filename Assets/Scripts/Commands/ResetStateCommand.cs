using Zenject;

namespace GameKit
{

	public class ResetStateCommand
	{
		[Inject] private LaunchCommand _launchCommand;

		public void Execute()
		{
			LocalStateProxy.Delete();
			_launchCommand.Execute();
		}
	}
}
