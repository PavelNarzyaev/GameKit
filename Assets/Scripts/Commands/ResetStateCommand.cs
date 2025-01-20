using Zenject;

public class ResetStateCommand
{
	[Inject] private LaunchCommand _launchCommand;

	public void Execute()
	{
		StateProxy.DeleteFile();
		_launchCommand.Execute();
	}
}
