using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace VirtualKeyboard
{
    public static class VirtualKeyboard
    {
        // SysWow64 리다이렉트 중지
        [DllImport("kernel32.dll")] static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);
        // SysWow64 리다이렉트 재개
        [DllImport("kernel32.dll")] static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

        public static void Open()
        {
            // Check already opened
            if (Process.GetProcessesByName("osk").Length > 0) return;

            IntPtr ptr = default;
            Wow64DisableWow64FsRedirection(ref ptr);
            Process.Start("osk.exe");
            Wow64RevertWow64FsRedirection(ptr);
        }

        public static void Close()
        {
            var processes = Process.GetProcessesByName("osk");
            if (processes.Length > 0) Array.ForEach(processes, process => process.Kill());
        }
    }
}