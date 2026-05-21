using System;
using System.Text;
using GameKit.Logs.Contracts;
using GameKit.UiDebugPanel.Contracts;
using GameKit.UiRegionsControl.Contracts;
using JetBrains.Annotations;

namespace GameKit.LogsDebugPage
{
    public enum LogsDebugPageFilter
    {
        All,
        Problems
    }

    [UsedImplicitly]
    public class LogsDebugPagePresenter : IDisposable
    {
        private LogsDebugPageFilter m_filter = LogsDebugPageFilter.All;

        private readonly ILogsProvider m_logsProvider;
        private readonly IDebugPanelMessageNavigator m_debugPanelMessageNavigator;

        public LogsDebugPageFilter CurrentFilter => m_filter;

        public event Action Changed;
        public event Action FilterChanged;

        public LogsDebugPagePresenter(
            ILogsProvider logsProvider,
            IDebugPanelMessageNavigator debugPanelMessageNavigator)
        {
            m_logsProvider = logsProvider;
            m_debugPanelMessageNavigator = debugPanelMessageNavigator;
            m_logsProvider.Changed += HandleLogsProviderChanged;
        }

        public void Dispose()
        {
            m_logsProvider.Changed -= HandleLogsProviderChanged;
        }

        private void HandleLogsProviderChanged()
        {
            Changed?.Invoke();
        }

        public void SetFilter(LogsDebugPageFilter filter)
        {
            if (m_filter == filter)
            {
                return;
            }

            m_filter = filter;
            FilterChanged?.Invoke();
            Changed?.Invoke();
        }

        public string GetLogsText()
        {
            return BuildLogsText(m_filter);
        }

        public void CopyAllLogs()
        {
            UniClipboard.SetText(BuildLogsText(LogsDebugPageFilter.All));
            m_debugPanelMessageNavigator.ShowMessage(UiRegionElementAddressableIds.k_DebugPanelMessage);
        }

        private string BuildLogsText(LogsDebugPageFilter filter)
        {
            var builder = new StringBuilder();

            foreach (var message in m_logsProvider.Messages)
            {
                if (filter == LogsDebugPageFilter.Problems && !message.IsProblem)
                {
                    continue;
                }

                builder.Append('[');
                builder.Append(message.Type);
                builder.Append("] ");
                builder.AppendLine(message.Condition);

                if (message.IsProblem)
                {
                    builder.AppendLine(message.StackTrace);
                }
            }

            return builder.ToString();
        }
    }
}
