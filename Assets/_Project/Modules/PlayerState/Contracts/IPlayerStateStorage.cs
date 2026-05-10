namespace GameKit.PlayerState.Contracts
{
    public interface IPlayerStateStorage
    {
        bool Exists();
        void Save(string stateJson);
        string Load();
        void Delete();
    }
}
