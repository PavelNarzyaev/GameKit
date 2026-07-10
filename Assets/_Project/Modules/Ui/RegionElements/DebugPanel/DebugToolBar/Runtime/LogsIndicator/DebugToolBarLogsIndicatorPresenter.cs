using System;
using GameKit.Logs.Contracts;
using GameKit.UiRegionsControl.Contracts;
using JetBrains.Annotations;
using R3;
using UnityEngine;

namespace GameKit.DebugToolBar
{
    public enum DebugToolBarLogsIndicatorState
    {
        Default,
        Warning,
        Error
    }

    [UsedImplicitly]
    public class DebugToolBarLogsIndicatorPresenter : IDisposable
    {
        private readonly ILogsProvider m_logsProvider;
        private readonly DebugToolBarPageTabsPresenter m_pageTabsPresenter;
        private readonly IDisposable m_messageAddedSubscription;

        public event Action StateChanged;

        public DebugToolBarLogsIndicatorState State { get; private set; }

        public DebugToolBarLogsIndicatorPresenter(ILogsProvider logsProvider, DebugToolBarPageTabsPresenter pageTabsPresenter)
        {
            m_logsProvider = logsProvider;
            m_pageTabsPresenter = pageTabsPresenter;
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

        private void SetState(DebugToolBarLogsIndicatorState state)
        {
            if (state <= State)
            {
                return;
            }

            State = state;
            StateChanged?.Invoke();
        }

        private DebugToolBarLogsIndicatorState GetState(LogType type)
        {
            return type switch
            {
                LogType.Warning => DebugToolBarLogsIndicatorState.Warning,
                LogType.Error or LogType.Assert or LogType.Exception => DebugToolBarLogsIndicatorState.Error,
                _ => DebugToolBarLogsIndicatorState.Default
            };
        }

        public void ShowLogsPage()
        {
            m_pageTabsPresenter.ShowPage(UiRegionElementAddressableIds.k_LogsDebugPage);
        }
    }
}
