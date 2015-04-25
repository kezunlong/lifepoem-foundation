using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IR.API.Foundation.Utilities.Logging
{
    /// <summary>
    /// Log message level enumeration.
    /// </summary>
    public enum LoggerLevel
    {
        Fatal = 0,
        Error,
        Warning,
        Info,
        Debug
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ILogger
    {
        void Fatal(String text);

        void Fatal(Exception ex);

        void Fatal(String text, Exception ex);

        void Error(String text);

        void Error(Exception ex);

        void Error(String text, Exception ex);

        void Warning(String text);

        void Warning(Exception ex);

        void Warning(String text, Exception ex);

        void Info(String text);

        void Info(Exception ex);

        void Info(String text, Exception ex);

        void Debug(String text);

        void Debug(Exception ex);

        void Debug(String text, Exception ex);
    }
}
