using System;

namespace GameKit.TimeOffset.Contracts
{
    public interface ITimeOffsetService
    {
        event Action Changed;
        int OffsetSeconds { get; }
        void AddSeconds(int deltaSeconds);
    }
}
