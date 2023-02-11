using System;
using System.Linq;
using System.IO;
using System.Threading;
using System.Collections.Concurrent;

namespace Log
{
    public static class Log
    {
        public enum LogType
        {
            Application,
            Communication,
            Data,
            Warning,
            Error,
            Exception,
        }

        static string LogPath = @"C:\Log\";
        static Thread LogThread;
        static BlockingCollection<(string Path, string Contents)> LogQueue = new BlockingCollection<(string Path, string Contents)>();

        /// <summary>
        /// Unit : Day
        /// </summary>
        public static int PreserveLog = 30;

        public static void LogStart()
        {
            LogThread = new Thread(LogWriteThread);
            LogThread.IsBackground = true;
            LogThread.Start();
        }

        static void LogWriteThread()
        {
            CheckLogPath();
            DeleteFileByTime();
            while (true)
            {
                (string path, string contents) = LogQueue.Take();
                File.AppendAllText(path, contents);
            }
        }

        public static void WriteLog(LogType logType, string message)
        {
            string logName = logType.ToString() + "_" + DateTime.Today.ToString("yyyy-MM-dd") + ".log";
            string totalLogPath = LogPath + logType.ToString() + @"\";
            switch (logType)
            {
                case LogType.Application:
                case LogType.Communication:
                case LogType.Data:
                case LogType.Warning:
                case LogType.Error:
                case LogType.Exception:
                    LogQueue.Add((totalLogPath + logName, $"{DateTime.Now.ToString("HH:mm:ss.fff")} - {message}\r\n"));
                    break;
            }
        }

        static void CheckLogPath()
        {
            var logTypes = Enum.GetNames(typeof(LogType)).ToList();
            foreach (var logType in logTypes)
            {
                string totalLogPath = LogPath + logType + @"\";
                CreateDirectory(totalLogPath);
            }
        }

        static void CreateDirectory(string totalLogPath)
        {
            if (!Directory.Exists(LogPath))
            {
                Directory.CreateDirectory(LogPath);
                Thread.Sleep(100);
            }
            if (!Directory.Exists(totalLogPath))
            {
                Directory.CreateDirectory(totalLogPath);
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Need to change this function in case of delete files everyday
        /// </summary>
        static void DeleteFileByTime()
        {
            var logTypes = Enum.GetNames(typeof(LogType)).ToList();
            foreach (var logType in logTypes)
            {
                string totalLogPath = LogPath + logType + @"\";
                try
                {
                    var dirInfo = new DirectoryInfo(totalLogPath);

                    var standardTime = DateTime.ParseExact(DateTime.Now.AddDays((-1 * PreserveLog)).ToString("yyyyMMdd"), "yyyyMMdd", null);

                    foreach (FileInfo file in dirInfo.GetFiles())
                    {
                        if (file.Name.Contains(logType))
                        {
                            if (DateTime.Compare(file.CreationTime, standardTime) < 0)
                            {
                                File.Delete(file.FullName);
                                WriteLog(LogType.Application, "Old log deleted automatically - path : " + file.FullName);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    WriteLog(LogType.Exception, "Log Delete - DeleteFileByTime failed - exception : " + e.ToString());
                }
            }
        }
    }
}