using System;
using GameKit.PlayerState.Contracts;
using JetBrains.Annotations;

namespace GameKit.PlayerState
{
    [UsedImplicitly]
    public class PlayerStateValidator : IPlayerStateValidator
    {
        public void Validate(PlayerStateDto state)
        {
            if (state == null || string.IsNullOrEmpty(state.UserId))
            {
                throw new FormatException("Saved state format is incompatible.");
            }
        }
    }
}
