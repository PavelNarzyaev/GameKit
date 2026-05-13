using GameKit.PlayerState.Contracts;

namespace GameKit.PlayerState
{
    public interface IPlayerStateFactory
    {
        PlayerStateDto Create();
    }
}
