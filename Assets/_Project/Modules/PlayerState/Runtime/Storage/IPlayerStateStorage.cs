namespace GameKit.PlayerState
{
    public interface IPlayerStateStorage
    {
        bool Exists();
        void Save(string stateJson);
        string Load();
        void Delete();
    }
}
