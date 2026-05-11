using System;
using System.Collections.Generic;
using GameKit.Logs.Contracts;
using JetBrains.Annotations;

namespace GameKit.Logs
{
    [UsedImplicitly]
    public class LogsProvider : ILogsProvider, ILogMessagesWriter
    {
        private readonly List<LogMessage> m_messages = new();

        public event Action Changed;

        public IReadOnlyList<LogMessage> Messages => m_messages;

        public void Add(LogMessage message)
        {
            m_messages.Add(message);
            Changed?.Invoke();
        }
    }
}
