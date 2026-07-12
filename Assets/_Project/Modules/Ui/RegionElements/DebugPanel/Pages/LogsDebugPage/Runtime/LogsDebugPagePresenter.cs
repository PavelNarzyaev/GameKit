using System;
using System.Text;
using GameKit.Logs.Contracts;
using GameKit.UiDebugPanel.Contracts;
using GameKit.UiRegionsControl.Contracts;
using JetBrains.Annotations;
using R3;
using UnityEngine;

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
        private readonly ILogsProvider m_logsProvider;
        private readonly IDebugPanelMessageNavigator m_debugPanelMessageNavigator;
        private readonly IDisposable m_messageAddedSubscription;

        public LogsDebugPageFilter CurrentFilter { get; private set; } = LogsDebugPageFilter.All;

        public event Action Changed;
        public event Action FilterChanged;

        public LogsDebugPagePresenter(
            ILogsProvider logsProvider,
            IDebugPanelMessageNavigator debugPanelMessageNavigator)
        {
            m_logsProvider = logsProvider;
            m_debugPanelMessageNavigator = debugPanelMessageNavigator;
            m_messageAddedSubscription = m_logsProvider.MessageAdded.Subscribe(HandleMessageAdded);
        }

        public void Dispose()
        {
            m_messageAddedSubscription.Dispose();
        }

        private void HandleMessageAdded(LogMessage message)
        {
            Changed?.Invoke();
        }

        public void SetFilter(LogsDebugPageFilter filter)
        {
            if (CurrentFilter == filter)
            {
                return;
            }

            CurrentFilter = filter;
            FilterChanged?.Invoke();
            Changed?.Invoke();
        }

        public string GetLogsText()
        {
            return BuildLogsText(CurrentFilter, true, false);
        }

        public void CopyAllLogs()
        {
            UniClipboard.SetText(BuildLogsText(LogsDebugPageFilter.All, false, true));
            m_debugPanelMessageNavigator.ShowMessage(UiRegionElementAddressableIds.k_DebugPanelMessage);
        }

        private string BuildLogsText(LogsDebugPageFilter filter, bool useRichText, bool addStackTrace)
        {
            var builder = new StringBuilder();

            foreach (var message in m_logsProvider.Messages)
            {
                if (filter == LogsDebugPageFilter.Problems && !message.IsProblem)
                {
                    continue;
                }

                AppendMessage(builder, message, useRichText, addStackTrace);
            }

            return builder.ToString();
        }

        private static void AppendMessage(StringBuilder builder, LogMessage message, bool useRichText, bool addStackTrace)
        {
            if (useRichText)
            {
                builder.Append("<color=");
                builder.Append(GetMessageColor(message.Type));
                builder.Append('>');
            }

            builder.Append('[');
            builder.Append(message.Type);
            builder.Append("] ");
            AppendText(builder, message.Condition, useRichText);

            if (addStackTrace && message.IsProblem)
            {
                builder.AppendLine();
                AppendText(builder, message.StackTrace, useRichText);
            }

            if (useRichText)
            {
                builder.Append("</color>");
            }

            builder.AppendLine();
        }

        private static void AppendText(StringBuilder builder, string text, bool useRichText)
        {
            if (useRichText)
            {
                builder.Append("<noparse>");
            }

            builder.Append(text);

            if (useRichText)
            {
                builder.Append("</noparse>");
            }
        }

        private static string GetMessageColor(LogType type)
        {
            return type switch
            {
                LogType.Warning => "yellow",
                LogType.Error => "red",
                LogType.Assert => "red",
                LogType.Exception => "red",
                _ => "white"
            };
        }
    }
}
