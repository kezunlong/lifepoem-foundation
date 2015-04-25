using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IR.API.Foundation.Utilities.Logging
{
    public abstract class DefaultLogger : ILogger
    {
        #region Properties

        /// <summary>
        /// Only levels under(more important than) this Level will be logged.
        /// </summary>
        public LoggerLevel Level { get; set; }

        #endregion

        #region Abstract and virtual methods

        protected abstract void WriteLineImp(string messageToWrite, LoggerLevel level);

        protected void WriteLine(LoggerLevel level, String text)
        {
            if (level <= Level)
                WriteLineImp(text, level);
        }

        protected void WriteLine(LoggerLevel level, IEnumerable<String> texts)
        {
            foreach (string text in texts)
                WriteLine(level, text);
        }

        protected void WriteLine(LoggerLevel level, string message, IEnumerable<String> texts)
        {
            WriteLine(level, message);
            foreach (string text in texts)
                WriteLine(level, text);
        }

        #endregion

        #region ILogger implementation

        public void Fatal(String text)
        {
            WriteLine(LoggerLevel.Fatal, text);
        }

        public void Fatal(Exception ex)
        {
            WriteLine(LoggerLevel.Fatal, FormatExceptionRecursively(ex));
        }

        public void Fatal(String text, Exception ex)
        {
            WriteLine(LoggerLevel.Fatal, text, FormatExceptionRecursively(ex));
        }

        public void Error(String text)
        {
            WriteLine(LoggerLevel.Error, text);
        }

        public void Error(Exception ex)
        {
            WriteLine(LoggerLevel.Error, FormatExceptionRecursively(ex));
        }

        public void Error(String text, Exception ex)
        {
            WriteLine(LoggerLevel.Error, text, FormatExceptionRecursively(ex));
        }

        public void Warning(String text)
        {
            WriteLine(LoggerLevel.Warning, text);
        }

        public void Warning(Exception ex)
        {
            WriteLine(LoggerLevel.Warning, FormatExceptionRecursively(ex));
        }

        public void Warning(String text, Exception ex)
        {
            WriteLine(LoggerLevel.Warning, text, FormatExceptionRecursively(ex));
        }

        public void Info(String text)
        {
            WriteLine(LoggerLevel.Info, text);
        }

        public void Info(Exception ex)
        {
            WriteLine(LoggerLevel.Info, FormatExceptionRecursively(ex));
        }

        public void Info(String text, Exception ex)
        {
            WriteLine(LoggerLevel.Info, text, FormatExceptionRecursively(ex));
        }

        public void Debug(String text)
        {
            WriteLine(LoggerLevel.Debug, text);
        }

        public void Debug(Exception ex)
        {
            WriteLine(LoggerLevel.Debug, FormatExceptionRecursively(ex));
        }

        public void Debug(String text, Exception ex)
        {
            WriteLine(LoggerLevel.Debug, text, FormatExceptionRecursively(ex));
        }

        #endregion

        #region helper methods

        /// <summary>
        /// Formats exception and all its inner exceptions.
        /// </summary>
        /// <param name="exception">Exception to process.</param>
        /// <returns>Formatted exception collection.</returns>
        protected IEnumerable<string> FormatExceptionRecursively(Exception exception)
        {
            int level = 0;
            foreach (Exception e in FlattenException(exception))
                yield return FormatException(e, level++);
        }

        /// <summary>
        /// Formats exception message and data. 
        /// </summary>
        /// <param name="exception">Exception to format.</param>
        /// <param name="level">Exception inner level (0 for top level).</param>
        /// <returns>Exception string represetation.</returns>
        protected string FormatException(Exception exception, int level)
        {
            // Format exception data
            StringBuilder sb = new StringBuilder();
            IDictionary dict = exception.Data;
            if (dict != null)
            {
                foreach (DictionaryEntry dEntry in dict)
                {
                    sb.AppendFormat(" data: [{0}] = [{1}]\r\n", dEntry.Key, dEntry.Value);
                }
            }

            // Get exception error code if present
            StringBuilder errorCode = new StringBuilder();
            if (exception is System.Runtime.InteropServices.ExternalException)
            {
                errorCode.AppendFormat(
                                " error code: {0}\r\n",
                                (exception as System.Runtime.InteropServices.ExternalException).ErrorCode);
            }

            // Format entire exception and return result
            string levelString = (level == 0)
                                     ? "Exception"
                                     : string.Format("InnerException(level={0})", level);
            string output = string.Format("{0}: {1}\r\n site: {2}\r\n source: {3}\r\n{4}{5}{6}",
                                          levelString, exception.Message, exception.TargetSite,
                                          exception.Source, errorCode, sb, exception.StackTrace
                );

            return output;

        }

        /// <summary>
        /// Returns exception and all its inner exceptions.
        /// </summary>
        /// <param name="exception">Exception to process.</param>
        /// <returns>Inner exception collection.</returns>
        protected IEnumerable<Exception> FlattenException(Exception exception)
        {
            while (exception != null)
            {
                yield return exception;
                exception = exception.InnerException;
            }
        }

        #endregion
    }
}
