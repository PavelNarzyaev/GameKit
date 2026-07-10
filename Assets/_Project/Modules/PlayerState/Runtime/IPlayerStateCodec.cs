using GameKit.PlayerState.Contracts;

namespace GameKit.PlayerState
{
    public interface IPlayerStateCodec
    {
        string Encode(PlayerStateDto state);
        PlayerStateDto Decode(string payload);
    }
}
