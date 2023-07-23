<h1 id="title">C# - Execute process (ex : osk.exe)</h1>

<h2 id="intro">Introduction</h2>

1. `System.Diagnostics.Process.Start()`를 통해 프로세스를 실행할 수 있다.
2. 64bit Windows 환경에서 32bit process 실행 시, `Environment.SystemDirectory` 경로를 자동으로 SysWOW64로 리다이렉트한다.
3. SysWOW64 리다이렉트 중단이 필요한 경우, `kernel32.dll`에서 제공하는 API를 이용하여 중지 / 재개가 가능하다.
4. 아래 예제에서 실행하는 `화상 키보드` `osk.exe`의 경우 System32에만 존재한다. (32bit process에서 접근하면 SysWOW64 리다이렉트가 실행을 방해한다.)

<br><br>

<h2 id="code">Code</h2>

```csharp
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
```

<br><br>

<h2 id="example">Use example</h2>

```csharp
namespace VirtualKeyboard
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VirtualKeyboard.Open();
            VirtualKeyboard.Close();
        }
    }
}
```