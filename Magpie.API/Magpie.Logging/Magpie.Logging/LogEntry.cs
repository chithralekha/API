using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magpie.Logging
{
    public struct LogEntry
    {
        private LoggingLevels loggingLevel;

        public LoggingLevels LoggingLevel { get { return loggingLevel; } }

        private string message;

        public string Message { get { return message; } }

        private int? tennantId;

        public int? TennantId { get { return tennantId; } }

        private Exception exception;

        public Exception Exception { get { return exception; } }

        public LogEntry(LoggingLevels LoggingLevel, string Message)
        {
            loggingLevel = LoggingLevel;
            message = Message;
            tennantId = null;
            exception = null;
        }

        public LogEntry(LoggingLevels LoggingLevel, string Message, int? TennantId)
        {
            loggingLevel = LoggingLevel;
            message = Message;
            tennantId = TennantId;
            exception = null;
        }

        public LogEntry(LoggingLevels LoggingLevel, string Message, Exception Exception)
        {
            loggingLevel = LoggingLevel;
            message = Message;
            tennantId = null;
            exception = Exception;
        }

        public LogEntry(LoggingLevels LoggingLevel, string Message, Exception Exception, int? TennantId)
        {
            loggingLevel = LoggingLevel;
            message = Message;
            tennantId = TennantId;
            exception = Exception;
        }
    }
}
