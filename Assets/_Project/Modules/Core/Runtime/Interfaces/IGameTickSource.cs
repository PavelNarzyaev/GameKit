using System;

namespace GameKit.Core
{
    public interface IGameTickSource
    {
        event Action Ticked;
    }
}
