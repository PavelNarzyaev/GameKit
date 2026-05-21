using System;
using GameKit.Logs.Contracts;
using GameKit.UiDebugPanel.Contracts;
using GameKit.UiRegionsControl.Contracts;
using UnityEngine;
using JetBrains.Annotations;

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

        private int m_viewedMessagesCount;

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
            m_logsProvider.Changed += HandleLogsProviderChanged;
            RefreshLogsIndicatorState();
        }

        public void Dispose()
        {
            m_debugPanelPageNavigator.PageChanged -= HandlePageChanged;
            m_logsProvider.Changed -= HandleLogsProviderChanged;
        }

        public void ShowPage(string addressableId)
        {
            m_debugPanelPageNavigator.Show(addressableId);
        }

        public void Close()
        {
            m_debugPanelPageNavigator.Close();
        }

        private void MarkLogsAsViewed()
        {
            m_viewedMessagesCount = m_logsProvider.Messages.Count;

            if (LogsIndicatorState == DebugPanelTabBarLogsIndicatorState.Default)
            {
                return;
            }

            LogsIndicatorState = DebugPanelTabBarLogsIndicatorState.Default;
            LogsIndicatorStateChanged?.Invoke();
        }

        private void HandlePageChanged()
        {
            if (IsLogsPageOpened())
            {
                MarkLogsAsViewed();
            }

            PageChanged?.Invoke();
        }

        private void HandleLogsProviderChanged()
        {
            RefreshLogsIndicatorState();
        }

        private void RefreshLogsIndicatorState()
        {
            if (IsLogsPageOpened())
            {
                MarkLogsAsViewed();
                return;
            }

            for (var i = m_viewedMessagesCount; i < m_logsProvider.Messages.Count; i++)
            {
                SetLogsIndicatorState(GetState(m_logsProvider.Messages[i].Type));
            }

            m_viewedMessagesCount = m_logsProvider.Messages.Count;
        }

        private void SetLogsIndicatorState(DebugPanelTabBarLogsIndicatorState state)
        {
            if (LogsIndicatorState == DebugPanelTabBarLogsIndicatorState.Error)
            {
                return;
            }

            if (LogsIndicatorState == DebugPanelTabBarLogsIndicatorState.Warning && state == DebugPanelTabBarLogsIndicatorState.Default)
            {
                return;
            }

            if (LogsIndicatorState == state)
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

        private bool IsLogsPageOpened()
        {
            return CurrentPageAddressableId == UiRegionElementAddressableIds.k_LogsDebugPage;
        }
    }
}
