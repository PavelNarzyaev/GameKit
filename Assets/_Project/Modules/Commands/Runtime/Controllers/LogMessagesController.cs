using System;
using GameKit.Logs.Contracts;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace GameKit.Commands
{
    [UsedImplicitly]
    public class LogMessagesController : IInitializable, IDisposable
    {
        private readonly ILogMessagesWriter m_logMessagesWriter;

        public LogMessagesController(ILogMessagesWriter logMessagesWriter)
        {
            m_logMessagesWriter = logMessagesWriter;
        }

        public void Initialize()
        {
#if !IS_PRODUCTION
            Application.logMessageReceived += HandleLogMessageReceived;
#endif
        }

        public void Dispose()
        {
#if !IS_PRODUCTION
            Application.logMessageReceived -= HandleLogMessageReceived;
#endif
        }

#if !IS_PRODUCTION
        private void HandleLogMessageReceived(string condition, string stackTrace, LogType type)
        {
            m_logMessagesWriter.Add(new LogMessage(condition, stackTrace, type));
        }
#endif
    }
}
