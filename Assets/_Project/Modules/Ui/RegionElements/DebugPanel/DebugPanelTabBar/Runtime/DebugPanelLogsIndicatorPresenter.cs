using System;
using GameKit.Logs.Contracts;
using JetBrains.Annotations;
using R3;
using UnityEngine;

namespace GameKit.DebugPanelTabBar
{
    public enum DebugPanelLogsIndicatorState
    {
        Default,
        Warning,
        Error
    }

    [UsedImplicitly]
    public class DebugPanelLogsIndicatorPresenter : IDisposable
    {
        private readonly ILogsProvider m_logsProvider;
        private readonly IDisposable m_messageAddedSubscription;

        public event Action StateChanged;

        public DebugPanelLogsIndicatorState State { get; private set; }

        public DebugPanelLogsIndicatorPresenter(ILogsProvider logsProvider)
        {
            m_logsProvider = logsProvider;
            m_messageAddedSubscription = m_logsProvider.MessageAdded.Subscribe(HandleMessageAdded);
            RefreshInitialState();
        }

        public void Dispose()
        {
            m_messageAddedSubscription.Dispose();
        }

        private void HandleMessageAdded(LogMessage message)
        {
            SetState(GetState(message.Type));
        }

        private void RefreshInitialState()
        {
            foreach (var message in m_logsProvider.Messages)
            {
                SetState(GetState(message.Type));
            }
        }

        private void SetState(DebugPanelLogsIndicatorState state)
        {
            if (state <= State)
            {
                return;
            }

            State = state;
            StateChanged?.Invoke();
        }

        private DebugPanelLogsIndicatorState GetState(LogType type)
        {
            return type switch
            {
                LogType.Warning => DebugPanelLogsIndicatorState.Warning,
                LogType.Error or LogType.Assert or LogType.Exception => DebugPanelLogsIndicatorState.Error,
                _ => DebugPanelLogsIndicatorState.Default
            };
        }
    }
}
