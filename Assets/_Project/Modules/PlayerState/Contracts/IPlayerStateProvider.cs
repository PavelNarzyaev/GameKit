using System;

namespace GameKit.PlayerState.Contracts
{
    public interface IPlayerStateProvider
    {
        PlayerStateDto Data { get; set; }
        bool IsDirty { get; }
        event Action RefreshedFromJson;

        void MarkAsDirty();
        void Save();
        void Set(string json);
        void Refresh();
        string Get();
        void Delete();
    }
}
