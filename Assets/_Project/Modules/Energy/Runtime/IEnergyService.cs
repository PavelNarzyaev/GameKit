using System;

namespace GameKit.Energy
{
    public interface IEnergyService
    {
        event Action Changed;

        int Energy { get; }
        bool IsRestorationInProgress { get; }

        bool TryAdd(int amount);
        bool TrySpend(int amount);
        TimeSpan GetRestorationTimer();
        void ProcessPendingRestoration();
    }
}
