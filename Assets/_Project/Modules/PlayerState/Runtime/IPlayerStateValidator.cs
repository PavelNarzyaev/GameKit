using GameKit.PlayerState.Contracts;

namespace GameKit.PlayerState
{
    public interface IPlayerStateValidator
    {
        void Validate(PlayerStateDto state);
    }
}
