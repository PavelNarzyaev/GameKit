using System;
using System.Collections.Generic;

namespace GameKit.Logs.Contracts
{
    public interface ILogsProvider
    {
        event Action Changed;

        IReadOnlyList<LogMessage> Messages { get; }
    }
}
