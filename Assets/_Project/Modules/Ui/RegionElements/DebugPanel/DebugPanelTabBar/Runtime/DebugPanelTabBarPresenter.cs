using System;
using GameKit.Logs.Contracts;
using GameKit.UiDebugPanel.Contracts;
using UnityEngine;
using JetBrains.Annotations;
using R3;

namespace GameKit.DebugPanelTabBar
{
    public enum DebugPanelTabBarLogsIndicatorState
    {
        Default,
        Warning,
        Error
    }

    [UsedImplicitly]
    public class DebugPanelTabBarPresenter : IDisposable
    {
        private readonly IDebugPanelPageNavigator m_debugPanelPageNavigator;
        private readonly ILogsProvider m_logsProvider;
        private readonly IDisposable m_messageAddedSubscription;

        public event Action PageChanged;
        public event Action LogsIndicatorStateChanged;

        public DebugPanelTabBarLogsIndicatorState LogsIndicatorState { get; private set; }

        public string CurrentPageAddressableId => m_debugPanelPageNavigator.CurrentPageAddressableId;

        public DebugPanelTabBarPresenter(
            IDebugPanelPageNavigator debugPanelPageNavigator,
            ILogsProvider logsProvider)
        {
            m_debugPanelPageNavigator = debugPanelPageNavigator;
            m_logsProvider = logsProvider;
            m_debugPanelPageNavigator.PageChanged += HandlePageChanged;
            m_messageAddedSubscription = m_logsProvider.MessageAdded.Subscribe(HandleMessageAdded);
            RefreshInitialLogsIndicatorState();
        }

        public void Dispose()
        {
            m_debugPanelPageNavigator.PageChanged -= HandlePageChanged;
            m_messageAddedSubscription.Dispose();
        }

        public void ShowPage(string addressableId)
        {
            m_debugPanelPageNavigator.Show(addressableId);
        }

        public void Close()
        {
            m_debugPanelPageNavigator.Close();
        }

        private void HandlePageChanged()
        {
            PageChanged?.Invoke();
        }

        private void HandleMessageAdded(LogMessage message)
        {
            SetLogsIndicatorState(GetState(message.Type));
        }

        private void RefreshInitialLogsIndicatorState()
        {
            foreach (var message in m_logsProvider.Messages)
            {
                SetLogsIndicatorState(GetState(message.Type));
            }
        }

        private void SetLogsIndicatorState(DebugPanelTabBarLogsIndicatorState state)
        {
            if (state <= LogsIndicatorState)
            {
                return;
            }

            LogsIndicatorState = state;
            LogsIndicatorStateChanged?.Invoke();
        }

        private DebugPanelTabBarLogsIndicatorState GetState(LogType type)
        {
            return type switch
            {
                LogType.Warning => DebugPanelTabBarLogsIndicatorState.Warning,
                LogType.Error or LogType.Assert or LogType.Exception => DebugPanelTabBarLogsIndicatorState.Error,
                _ => DebugPanelTabBarLogsIndicatorState.Default
            };
        }
    }
}
