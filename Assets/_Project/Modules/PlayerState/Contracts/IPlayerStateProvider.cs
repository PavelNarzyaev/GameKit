using System;

namespace GameKit.PlayerState.Contracts
{
    public interface IPlayerStateProvider
    {
        PlayerStateDto Data { get; }
        bool IsDirty { get; }
        event Action Replaced;

        void Edit(Action<PlayerStateDto> edit);
        void Save();
        void ReplaceFromJson(string json);
        void Refresh();
        string ExportJson();
        void Delete();
    }
}
