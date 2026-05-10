namespace GameKit.Commands
{
    public interface IDestroyUiCommand
    {
        void Execute();
    }

    public interface ILaunchCommand
    {
        void Execute();
    }

    public interface IResetSceneCommand
    {
        void Execute();
    }

    public interface IResetStateCommand
    {
        void Execute();
    }

    public interface IShowInitialUiCommand
    {
        void Execute();
    }
}
