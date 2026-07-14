namespace GameKit.Commands.Contracts
{
    public interface ILaunchCommand
    {
        void Execute();
    }

    public interface IResetSceneCommand
    {
        void Execute();
    }

    public interface IShowInitialUiCommand
    {
        void Execute();
    }
}
