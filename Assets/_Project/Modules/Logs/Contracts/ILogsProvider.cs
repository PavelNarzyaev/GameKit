using System.Collections.Generic;
using R3;

namespace GameKit.Logs.Contracts
{
    public interface ILogsProvider
    {
        Observable<LogMessage> MessageAdded { get; }

        IReadOnlyList<LogMessage> Messages { get; }
    }
}
