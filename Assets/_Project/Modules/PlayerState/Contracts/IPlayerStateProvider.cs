using System;
using R3;

namespace GameKit.PlayerState.Contracts
{
    public interface IPlayerStateProvider
    {
        bool IsDirty { get; }
        event Action Replaced;

        string UserId { get; }
        long FirstLaunchTimestamp { get; }
        int LaunchesCounter { get; }
        ReadOnlyReactiveProperty<int> TimeOffsetSeconds { get; }
        ReadOnlyReactiveProperty<int> SoftCurrency { get; }
        ReadOnlyReactiveProperty<int> HardCurrency { get; }
        ReadOnlyReactiveProperty<int> Energy { get; }
        ReadOnlyReactiveProperty<long> EnergyNextRestoreTimestamp { get; }

        void IncrementLaunchesCounter();
        void SetTimeOffsetSeconds(int value);
        void SetSoftCurrency(int value);
        void SetHardCurrency(int value);
        void SetEnergyState(int energy, long nextRestoreTimestamp);
        void Save();
        void Reset();
        void ReplaceFromJson(string json);
        void Refresh();
        string ExportJson();
    }
}
