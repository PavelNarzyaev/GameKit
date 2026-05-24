using System;
using GameKit.UiReset.Contracts;

namespace GameKit.UiReset
{
    public class UiResetEventBus : IUiResetEventPublisher, IUiResetEventListener
    {
        public event Action ResetRequested;

        public void PublishReset()
        {
            ResetRequested?.Invoke();
        }
    }
}
