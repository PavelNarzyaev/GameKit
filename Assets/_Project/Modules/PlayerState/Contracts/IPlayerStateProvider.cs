using System;
using GameKit.Currencies.Contracts;
using R3;

namespace GameKit.PlayerState.Contracts
{
    public interface IPlayerStateProvider
    {
        PlayerStateDto Data { get; }
        bool IsDirty { get; }
        event Action Replaced;

        ReadOnlyReactiveProperty<int> GetSoftCurrency();
        ReadOnlyReactiveProperty<int> GetHardCurrency();

        void Edit(Action<PlayerStateDto> edit);
        void Save();
        void Reset();
        void ReplaceFromJson(string json);
        void Refresh();
        string ExportJson();
    }
}
