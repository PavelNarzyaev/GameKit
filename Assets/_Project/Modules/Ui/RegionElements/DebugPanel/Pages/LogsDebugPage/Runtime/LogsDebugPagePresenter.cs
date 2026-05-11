using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace GameKit.LogsDebugPage
{
    [UsedImplicitly]
    public class LogsDebugPagePresenter : IInitializable, IDisposable
    {
        private readonly List<LogEntry> m_entries = new();
        private LogFilter m_filter = LogFilter.All;

        public event Action Changed;

        public void Initialize()
        {
            Application.logMessageReceived += HandleLogMessageReceived;
        }

        public void Dispose()
        {
            Application.logMessageReceived -= HandleLogMessageReceived;
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

        private void HandleLogMessageReceived(string condition, string stackTrace, LogType type)
        {
            m_entries.Add(new LogEntry(condition, stackTrace, type));
            Changed?.Invoke();
        }

        private string BuildLogsText(LogFilter filter)
        {
            var builder = new StringBuilder();

            foreach (var entry in m_entries)
            {
                if (filter == LogFilter.Problems && !entry.IsProblem)
                {
                    continue;
                }

                builder.Append('[');
                builder.Append(entry.Type);
                builder.Append("] ");
                builder.AppendLine(entry.Condition);

                if (entry.IsProblem)
                {
                    builder.AppendLine(entry.StackTrace);
                }
            }

            return builder.ToString();
        }

        private readonly struct LogEntry
        {
            public LogEntry(string condition, string stackTrace, LogType type)
            {
                Condition = condition;
                StackTrace = stackTrace;
                Type = type;
            }

            public string Condition { get; }
            public string StackTrace { get; }
            public LogType Type { get; }

            public bool IsProblem => Type != LogType.Log;
        }

        private enum LogFilter
        {
            All,
            Problems
        }
    }
}
