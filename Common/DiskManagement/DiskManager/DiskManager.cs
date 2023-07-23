using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using static Log.Log;

namespace DiskManager
{
    public static class DiskManager
    {
        static string DefaultPath = @"C:\Log\";
        static Thread ManagerWorker;
        static bool IsRun = false;

        public static int DiskPreserve { get; set; } = 20;

        /// <summary>
        /// Start disk management thread
        /// </summary>
        /// <param name="diskPreserve">Percent value will be preserved</param>
        public static void StartManager(int diskPreserve)
        {
            DiskPreserve = diskPreserve;
            ManagerWorker = new Thread(ManagerThread);
            ManagerWorker.IsBackground = true;
            IsRun = true;
            ManagerWorker.Start();
            WriteLog(LogType.Application, "Disk manager started");
        }

        public static void StopManager()
        {
            IsRun = false;
            WriteLog(LogType.Application, "Disk manager stopped");
        }

        public static bool IsManagerRun() => IsRun;

        static void ManagerThread()
        {
            while (IsRun)
            {
                if (!IsFreeSpaceEnough(DiskPreserve, "C"))
                {
                    // 가장 용량 많은 폴더에서 하나 지우기
                    var logTypes = Enum.GetNames(typeof(LogType)).ToList();
                    List<long> sizes = new List<long>();
                    foreach (var logType in logTypes) sizes.Add(GetDirectorySize(DefaultPath + logType + "\\"));

                    int selectedIndex = 0;
                    if (logTypes.Count > 1)
                    {
                        for (int i = 0; i < logTypes.Count - 1; i++)
                        {
                            if (sizes[i] < sizes[i + 1]) selectedIndex = i;
                        }
                    }
                    DeleteOneFile(DefaultPath + logTypes[selectedIndex] + "\\", logTypes[selectedIndex]);
                    Thread.Sleep(2000);
                }
                else
                {
                    Thread.Sleep(2000);
                }
            }
        }

        /// <summary>
        /// return true when free space is enough
        /// </summary>
        /// <param name="preserveSize">Percent value will be preserved</param>
        static bool IsFreeSpaceEnough(int preserveSize, string path)
        {
            var drives = DriveInfo.GetDrives();

            foreach (var drive in drives)
            {
                if (drive.DriveType == DriveType.Fixed && drive.Name.Contains(path))
                {
                    if (drive.AvailableFreeSpace / Math.Pow(1024, 3) < drive.TotalSize / Math.Pow(1024, 3) * ((double)preserveSize / 100))
                        return false;
                }
            }
            return true;
        }

        static long GetDirectorySize(string path)
        {
            long size = 0;
            DirectoryInfo dirInfo = new DirectoryInfo(path);

            foreach (FileInfo fi in dirInfo.GetFiles("*", SearchOption.AllDirectories)) size += fi.Length;
            return size;
        }

        /// <summary>
        /// Delete only log created by SW
        /// </summary>
        /// <param name="path"></param>
        /// <param name="logType"></param>
        static void DeleteOneFile(string path, string logType)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                DateTime time = DateTime.Now;
                FileInfo deleteFileInfo = null;

                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    if (file.Name.Contains(logType))
                    {
                        if (DateTime.Compare(file.CreationTime, time) < 0)
                        {
                            time = file.CreationTime;
                            deleteFileInfo = file;
                        }
                    }
                }
                if (deleteFileInfo != null)
                {
                    File.Delete(deleteFileInfo.FullName);
                    WriteLog(LogType.Application, "Old log deleted automatically by DiskManager - path : " + deleteFileInfo.FullName);
                }
                else
                {
                    WriteLog(LogType.Error, "Could not delete old log automatically by DiskManager - path : " + deleteFileInfo.FullName);
                }
            }
            catch (Exception e)
            {
                WriteLog(LogType.Exception, "Log Delete - DiskManager DeleteFile failed - exception : " + e.ToString());
            }
        }
    }
}