using System;

namespace GameKit.Core.Contracts
{
    public interface IGameTickSource
    {
        event Action Ticked;
    }
}
