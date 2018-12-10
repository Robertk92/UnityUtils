using UnityEngine;

namespace Utils.Diagnostics
{
    public struct ScreenMessage
    {
        public readonly string Message;
        public readonly string StackLine;
        public readonly LogType LogType;

        public ScreenMessage(string message, string stackLine, LogType logType)
        {
            this.Message = message;
            this.LogType = logType;
            this.StackLine = stackLine;
        }
    }
}