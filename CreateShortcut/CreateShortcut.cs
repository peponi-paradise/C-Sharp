using System;
using System.IO;
using System.Windows.Forms;
using IWshRuntimeLibrary;

namespace PersonalPlanner.Utility.Windows
{
    public static class Shortcut
    {
        private static string AppStartMenuPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Programs), "PersonalPlanner");

        public static bool ShortcutExist()
        {
            if (Directory.Exists(AppStartMenuPath)) return true;
            else return false;
        }

        public static void AddShortcut()
        {
            if (!Directory.Exists(AppStartMenuPath))
                Directory.CreateDirectory(AppStartMenuPath);

            string exeLocation = Path.Combine(AppStartMenuPath, "PersonalPlanner.exe" + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(exeLocation);

            shortcut.Description = @$"Personal Planner by ClockStrikes";
            shortcut.IconLocation = Application.StartupPath + "ShortDate.ico";
            shortcut.TargetPath = Application.ExecutablePath;
            shortcut.WorkingDirectory = Application.StartupPath;
            shortcut.Save();
        }
    }
}