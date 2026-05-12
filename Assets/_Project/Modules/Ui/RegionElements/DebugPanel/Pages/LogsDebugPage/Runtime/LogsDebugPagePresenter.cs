using System;
using System.Text;
using GameKit.Logs.Contracts;
using JetBrains.Annotations;
using Zenject;

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

        [Inject] private ILogsProvider m_logsProvider;

        public LogsDebugPageFilter CurrentFilter => m_filter;

        public event Action Changed;
        public event Action FilterChanged;

        [Inject]
        private void Inject()
        {
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
