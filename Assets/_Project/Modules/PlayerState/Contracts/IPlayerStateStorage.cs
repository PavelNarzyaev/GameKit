namespace GameKit.PlayerState.Contracts
{
    public interface IPlayerStateStorage
    {
        bool Exists();
        void Save(PlayerStateDto state);
        PlayerStateDto Load();
        void Delete();
    }
}
