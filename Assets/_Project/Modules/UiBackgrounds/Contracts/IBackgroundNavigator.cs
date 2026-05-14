using System;

namespace GameKit.UiBackgrounds.Contracts
{
    public interface IBackgroundNavigator
    {
        string CurrentBackgroundAddressableId { get; }
        event Action BackgroundChanged;

        void ShowBackground(string addressableId);
        void Reset();
    }
}
