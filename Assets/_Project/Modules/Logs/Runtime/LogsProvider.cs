using System;
using System.Collections.Generic;
using GameKit.Logs.Contracts;
using JetBrains.Annotations;
using R3;

namespace GameKit.Logs
{
    [UsedImplicitly]
    public class LogsProvider : ILogsProvider, ILogMessagesWriter, IDisposable
    {
        private readonly Subject<LogMessage> m_messageAdded = new();
        private readonly List<LogMessage> m_messages = new();

        public Observable<LogMessage> MessageAdded => m_messageAdded;
        public IReadOnlyList<LogMessage> Messages => m_messages;

        public void Add(LogMessage message)
        {
            m_messages.Add(message);
            m_messageAdded.OnNext(message);
        }

        public void Dispose()
        {
            m_messageAdded.Dispose();
        }
    }
}
