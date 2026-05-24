using System;

namespace GameKit.UiReset.Contracts
{
    public interface IUiResetEventPublisher
    {
        void PublishReset();
    }

    public interface IUiResetEventListener
    {
        event Action ResetRequested;
    }
}
