using System;

namespace GameKit.PlayerState.Contracts
{
    public interface IPlayerStateProvider
    {
        PlayerStateDto Data { get; set; }
        bool IsDirty { get; }
        event Action Replaced;

        void MarkAsDirty();
        void Save();
        void ReplaceFromJson(string json);
        void Refresh();
        string ExportJson();
        void Delete();
    }
}
