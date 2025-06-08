using Proxies;
using Zenject;

namespace Commands
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
