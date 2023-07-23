<h1 id="title">C# - Log 구현</h1>

<h2 id="intro">Introduction</h2>

1. 일반적으로 log4net 등의 훌륭한 로그 nuget이 있지만, 용도에 따라 migration이 어려울 때가 있다.
    - 특정 길이를 가지는 로그 작성 (일정 구간마다 파일을 끊어주거나 하는 경우 등)
    - 로그 포맷이 들쭉날쭉한 경우
    - etc...
2. 이와 같은 경우를 대비하여 로그 클래스를 하나쯤 구현해두면 두고두고 쓰기 좋다.

<br><br>

<h2 id="code">Code</h2>

```csharp
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
```

<br><br>

<h2 id="example">Use example</h2>

```csharp
namespace LogBase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Log.Log.LogStart();     // Log thread start. Create log path & check old logs.

            Log.Log.WriteLog(Log.Log.LogType.Application, "Log message");       // Write log
        }
    }
}
```