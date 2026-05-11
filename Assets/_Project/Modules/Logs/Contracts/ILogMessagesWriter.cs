namespace GameKit.Logs.Contracts
{
    public interface ILogMessagesWriter
    {
        void Add(LogMessage message);
    }
}
