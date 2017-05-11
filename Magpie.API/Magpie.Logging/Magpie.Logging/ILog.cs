using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magpie.Logging
{
    public enum LoggingLevels { Debug, Error, Fatal, Info, Warning }
    
    public interface ILog
    {
        void Debug(string Message, Exception Exception = null, int? TennantId = null);
        void Error(string Message, Exception Exception = null, int? TennantId = null);
        void Fatal(string Message, Exception Exception = null, int? TennantId = null);
        void Info(string Message, Exception Exception = null, int? TennantId = null);
        void Warn(string Message, Exception Exception = null, int? TennantId = null);
    }
}
