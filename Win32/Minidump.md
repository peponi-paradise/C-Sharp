## Introduction

<br>

- SW 운용 중 예상치 못한 exception은 언제나 발생한다.
- 만약 pdb 파일과 함께 프로세스가 실행중이라면 코드상의 어느 라인에서 예외가 발생했는지까지 윈도우 이벤트 로그를 통해 확인 가능하지만, 대략적인 정보만이 넘어오게 된다.
- 이 때, dump 파일의 정보가 있으면 비주얼 스튜디오 등을 통해 해당 시점의 SW 상태를 자세히 알 수 있어 분석이 빠르게 진행된다.

<br>

## Example - Declaration of minidump writer

<br>

- 아래는 구현할 `Dump()` 메서드의 간략한 정보이다.

|Return type|Method|Description|
|---|---|---|
|void|Dump()|Make minidumps. (Dividen mini and full dump files)<br>Dump type information:<br>1. *.mini.dmp : MiniDumpType.Normal (0x0)<br>2. *.full.dmp : MiniDumpType.Normal (0x0) \|<br>MiniDumpType.WithFullMemory (0x2) \|<br>MiniDumpType.WithHandleData (0x4) \|<br>MiniDumpType.WithProcessThreadData (0x100) \|<br>MiniDumpType.WithThreadInfo (0x1000) \|<br>MiniDumpType.WithCodeSegs (0x2000)|

- 아래는 Minidump 클래스 예시이다.

```cs
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Peponi.Utility.MiniDump;

/// <summary>
/// <code>
/// Non-UI thread : AppDomain.CurrentDomain.UnhandledException
/// WinForm UI thread : Application.ThreadException
/// WPF UI thread : Application.Current.DispatcherUnhandledException
/// ASP.NET HttpApplication request exception : HttpApplication.Error
/// </code>
/// </summary>
public static class MiniDumpWriter
{
    [Flags]
    private enum MiniDumpType
    {
        Normal = 0x00000000,
        WithDataSegs = 0x00000001,
        WithFullMemory = 0x00000002,
        WithHandleData = 0x00000004,
        FilterMemory = 0x00000008,
        ScanMemory = 0x00000010,
        WithUnloadedModules = 0x00000020,
        WithIndirectlyReferencedMemory = 0x00000040,
        FilterModulePaths = 0x00000080,
        WithProcessThreadData = 0x00000100,
        WithPrivateReadWriteMemory = 0x00000200,
        WithoutOptionalData = 0x00000400,
        WithFullMemoryInfo = 0x00000800,
        WithThreadInfo = 0x00001000,
        WithCodeSegs = 0x00002000,
        WithoutAuxiliaryState = 0x00004000,
        WithFullAuxiliaryState = 0x00008000
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    private struct MinidumpExceptionInformation
    {
        public uint ThreadId;
        public IntPtr ExceptionPointers;
        public int ClientPointers;
    }

    [DllImport("dbghelp.dll", EntryPoint = "MiniDumpWriteDump",
        CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode,
        ExactSpelling = true, SetLastError = true)]
    private static extern bool MiniDumpWriteDump(
                                IntPtr hProcess,
                                Int32 processId,
                                IntPtr fileHandle,
                                uint dumpType,
                                ref MinidumpExceptionInformation exceptionInfo,
                                IntPtr userInfo,
                                IntPtr extInfo);

    [DllImport("kernel32.dll")]
    private static extern uint GetCurrentThreadId();

    public static void Dump()
    {
        MakeMiniDump();
        MakeFullDump();
    }

    private static void MakeMiniDump()
    {
        var dumpPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"{DateTime.Now.ToString("yyMMdd-HHmmss")}.mini.dmp");
        var dumpType = MiniDumpType.Normal;
        MinidumpExceptionInformation exceptionInfo = new();
        exceptionInfo.ClientPointers = 1;
        exceptionInfo.ExceptionPointers = Marshal.GetExceptionPointers();
        exceptionInfo.ThreadId = GetCurrentThreadId();

        using (FileStream stream = new FileStream(dumpPath, FileMode.Create))
        {
            Process process = Process.GetCurrentProcess();

            MiniDumpWriteDump(process.Handle,
                                       process.Id,
                                       stream.SafeFileHandle.DangerousGetHandle(),
                                       (uint)dumpType,
                                       ref exceptionInfo,
                                       IntPtr.Zero,
                                       IntPtr.Zero);
        }
    }

    private static void MakeFullDump()
    {
        var dumpPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"{DateTime.Now.ToString("yyMMdd-HHmmss")}.full.dmp");
        var dumpType = MiniDumpType.Normal |
                             MiniDumpType.WithFullMemory |
                             MiniDumpType.WithHandleData |
                             MiniDumpType.WithProcessThreadData |
                             MiniDumpType.WithThreadInfo |
                             MiniDumpType.WithCodeSegs;
        MinidumpExceptionInformation exceptionInfo = new();
        exceptionInfo.ClientPointers = 1;
        exceptionInfo.ExceptionPointers = Marshal.GetExceptionPointers();
        exceptionInfo.ThreadId = GetCurrentThreadId();

        using (FileStream stream = new FileStream(dumpPath, FileMode.Create))
        {
            Process process = Process.GetCurrentProcess();

            MiniDumpWriteDump(process.Handle,
                                       process.Id,
                                       stream.SafeFileHandle.DangerousGetHandle(),
                                       (uint)dumpType,
                                       ref exceptionInfo,
                                       IntPtr.Zero,
                                       IntPtr.Zero);
        }
    }
}
```

<br>

## Example - Usage

<br>

- 아래는 `Example - Declaration of minidump writer`의 클래스를 이용하여 dump 파일을 남기는 예시이다.

```cs
using Peponi.Utility.MiniDump;

namespace Peponi.Utility.Tests;

[TestClass]
public class Minidump
{
    [TestMethod]
    public void Dump()
    {
        try
        {
            // Assume an exception occurred.
            throw new Exception("Test");
        }
        catch (Exception ex)
        {
            // Write dump files
            MiniDumpWriter.Dump();
        }
    }
}
```

- `Example - Declaration of minidump writer` 클래스의 comment는 처리되지 않은 예외에 대한 이벤트의 목록이다. (Application start 이전에 이벤트 핸들러를 등록해야 한다.)
    1. Non-UI thread : AppDomain.CurrentDomain.UnhandledException
    2. WinForm UI thread : Application.ThreadException
    3. WPF UI thread : Application.Current.DispatcherUnhandledException
    4. ASP.NET HttpApplication request exception : HttpApplication.Error
    - 이를 이용하여 UI 또는 non-UI thread에서 발생하는 처리되지 않은 예외를 dump로 상세히 기록할 수 있다.

<br>

## 참조 자료

<br>

- [MiniDumpWriteDump 함수(minidumpapiset.h)](https://learn.microsoft.com/ko-kr/windows/win32/api/minidumpapiset/nf-minidumpapiset-minidumpwritedump)
- [Generating .NET crash dumps automatically](https://stackoverflow.com/questions/1134048/generating-net-crash-dumps-automatically)
- [Peponi Library](https://github.com/peponi-paradise/Peponi)
- [디버깅 기술: 6. .NET 예외 처리 정리](https://www.sysnet.pe.kr/2/0/316)
- [.NET Framework: 110. WPF - 전역 예외 처리](https://www.sysnet.pe.kr/2/0/614)