using UnityEngine;

namespace GameKit.Logs.Contracts
{
    public readonly struct LogMessage
    {
        public LogMessage(string condition, string stackTrace, LogType type)
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
}
