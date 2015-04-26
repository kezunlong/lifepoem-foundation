using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Lifepoem.Foundation.Utilities.Logging
{
    /// <summary>
    /// File logger implementation
    /// </summary>
    public class Logger : DefaultLogger
    {
        #region Constructor

        public Logger(string loggerName)
            : this(AppDomain.CurrentDomain.BaseDirectory, loggerName)
        {
        }

        public Logger(string loggerDirectory, string loggerName)
            : this(loggerDirectory, loggerName, LoggerLevel.Info)
        {
        }

        public Logger(string loggerDirectory, string loggerName, LoggerLevel level)
        {
            // Get Logger File
            loggerFile = Path.Combine(loggerDirectory, loggerName);
            if (!(Path.GetExtension(LoggerFile) == ".log" || Path.GetExtension(LoggerFile) == ".txt"))
            {
                loggerFile += ".log";
            }

            if (!File.Exists(LoggerFile))
            {
                if(!Directory.Exists(loggerDirectory))
                    Directory.CreateDirectory(loggerDirectory);

                // create logging file and close stream at once
                File.Create(LoggerFile).Close(); 
            }

            Level = level;
        }

        #endregion

        #region Properties

        private string loggerFile;

        /// <summary>
        /// Gets the full path of the log file.
        /// </summary>
        public string LoggerFile 
        {
            get { return loggerFile; }
        }

        protected object lockObject = new object();

        private int maxiumLogFiles = 10;

        /// <summary>
        /// Gets or sets the maxium log files.
        /// </summary>
        public int MaxiumLogFiles
        {
            get { return maxiumLogFiles; }
            set { maxiumLogFiles = value; }
        }

        private int logFileSize = 2;

        /// <summary>
        /// Gets or sets the maxium log file size.
        /// </summary>
        public int LogFileSize 
        {
            get { return logFileSize; }
            set { logFileSize = value; }
        }

        #endregion

        #region DefaultLogger implementation

        protected override void WriteLineImp(string messageToWrite, LoggerLevel level)
        {
            lock (lockObject)
            {
                string messageFormat = "[{0}] [{1}/{2}] [{3}]: {4}";
                messageFormat = string.Format(
                    messageFormat,
                    level.ToString(),
                    Process.GetCurrentProcess().Id,
                    System.Threading.Thread.CurrentThread.ManagedThreadId,
                    DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    messageToWrite);

                try
                {
                    ManageLoggerFileSize();

                    using (StreamWriter sw = new StreamWriter(LoggerFile, true))
                    {
                        sw.WriteLine(messageFormat);
                    }
                }
                catch (Exception e)
                {
                    Trace.Write(e.Message);
                    Trace.Write(e.StackTrace);
                }
            }
        }

        #endregion

        #region Log file management

        private void ManageLoggerFileSize()
        {
            int fireRenameLogger = 0;
            Random random = new Random();
            if (random.Next(0, 9) == fireRenameLogger)
            {
                if (File.Exists(LoggerFile))
                {
                    FileInfo fileInfo = new FileInfo(LoggerFile);
                    long size = fileInfo.Length;
                    if (size / 1024 / 1024 >= LogFileSize)
                    {
                        MoveLoggerFilesChain();
                    }
                }
            }
        }

        private int GetTotalLoggerFiles()
        {
            int i = 1;
            while (File.Exists(LoggerFile + i))
            {
                i++;
            }
            return i - 1;
        }

        private void MoveLoggerFilesChain()
        {
            try
            {
                int totalLoggerFiles = GetTotalLoggerFiles();
                if (totalLoggerFiles >= maxiumLogFiles)
                {
                    while (totalLoggerFiles >= maxiumLogFiles)
                    {
                        File.Delete(LoggerFile + totalLoggerFiles);
                        totalLoggerFiles--;
                    }
                }

                while (totalLoggerFiles >= 1)
                {
                    File.Move(LoggerFile + totalLoggerFiles,
                              LoggerFile + (totalLoggerFiles + 1));

                    totalLoggerFiles--;
                }
                File.Move(LoggerFile, LoggerFile + 1);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                Trace.WriteLine(e.StackTrace);
            }
        }

        #endregion
    }
}
