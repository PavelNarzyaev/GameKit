using System;
using System.Text;
using GameKit.Logs.Contracts;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.LogsDebugPage
{
    [UsedImplicitly]
    public class LogsDebugPagePresenter : IDisposable
    {
        private LogFilter m_filter = LogFilter.All;

        [Inject] private ILogsProvider m_logsProvider;

        public event Action Changed;

        private enum LogFilter
        {
            All,
            Problems
        }

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

        public void ShowAll()
        {
            m_filter = LogFilter.All;
            Changed?.Invoke();
        }

        public void ShowProblems()
        {
            m_filter = LogFilter.Problems;
            Changed?.Invoke();
        }

        public string GetLogsText()
        {
            return BuildLogsText(m_filter);
        }

        public void CopyAllLogs()
        {
            UniClipboard.SetText(BuildLogsText(LogFilter.All));
        }

        private string BuildLogsText(LogFilter filter)
        {
            var builder = new StringBuilder();

            foreach (var message in m_logsProvider.Messages)
            {
                if (filter == LogFilter.Problems && !message.IsProblem)
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
