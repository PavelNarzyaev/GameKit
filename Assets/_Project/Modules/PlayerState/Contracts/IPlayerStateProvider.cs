using System;
using R3;

namespace GameKit.PlayerState.Contracts
{
    public interface IPlayerStateProvider
    {
        PlayerStateDto Data { get; }
        bool IsDirty { get; }
        event Action Replaced;

        ReadOnlyReactiveProperty<int> SoftCurrency { get; }
        ReadOnlyReactiveProperty<int> HardCurrency { get; }

        void SetSoftCurrency(int value);
        void SetHardCurrency(int value);
        void Edit(Action<PlayerStateDto> edit);
        void Save();
        void Reset();
        void ReplaceFromJson(string json);
        void Refresh();
        string ExportJson();
    }
}
