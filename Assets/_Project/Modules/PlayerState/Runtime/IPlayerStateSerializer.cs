using GameKit.PlayerState.Contracts;

namespace GameKit.PlayerState
{
    public interface IPlayerStateSerializer
    {
        string Serialize(PlayerStateDto state);
        PlayerStateDto Deserialize(string json);
    }
}
