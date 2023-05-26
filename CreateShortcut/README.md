## Introduction

<br>

- 아래는 코드를 이용해 바로가기를 생성하는 방법이다.
- COM 라이브러리 중 `Windows Script Host Object Model`을 추가한 후, `IWshRuntimeLibrary`를 using 선언하여 사용한다.

<br>

## Code

<br>

```cs
using System;
using System.IO;
using System.Windows.Forms;
using IWshRuntimeLibrary;

namespace CreateShortcut
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
```