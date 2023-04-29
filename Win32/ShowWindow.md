## Introduction

<br>

- 다음은 코드를 이용해 `Form 상태`를 지정하는 방법이다.
- Win32의 `user32.dll`을 이용하여 form을 조작한다.

<br>

## Code

<br>

```cs
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Win32Test
{
    public static class GUI
    {
        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, uint nCmdShow);

        private const uint SW_RESTORE = 0x09;

        public static int SetWindow(Form form, uint statusCode) => ShowWindow(form.Handle, statusCode);

        public static int RestoreWindow(Form form)
        {
            if (form.WindowState == FormWindowState.Minimized || form.WindowState == FormWindowState.Maximized)
            {
                ShowWindow(form.Handle, SW_RESTORE);
            }
        }
    }
}
```

<br>

## `nCmdShow` 항목

<br>

<table aria-label="테이블 1" class="table table-sm">
<thead>
<tr>
<th>값</th>
<th>의미</th>
</tr>
</thead>
<tbody>
<tr>
<td><strong>SW_HIDE</strong><br>0</td>
<td>창을 숨기고 다른 창을 활성화합니다.</td>
</tr>
<tr>
<td><strong>SW_SHOWNORMAL</strong><br><strong>SW_NORMAL</strong><br>1</td>
<td>창을 활성화하고 표시합니다. 창이 최소화되거나 최대화되면 시스템은 창을 원래 크기와 위치로 복원합니다. 처음으로 창을 표시할 때 애플리케이션에서 이 플래그를 지정해야 합니다.</td>
</tr>
<tr>
<td><strong>SW_SHOWMINIMIZED</strong><br>2</td>
<td>창을 활성화하고 최소화된 창으로 표시합니다.</td>
</tr>
<tr>
<td><strong>SW_SHOWMAXIMIZED</strong><br><strong>SW_MAXIMIZE</strong><br>3</td>
<td>창을 활성화하고 최대화된 창으로 표시합니다.</td>
</tr>
<tr>
<td><strong>SW_SHOWNOACTIVATE</strong><br>4</td>
<td>창의 가장 최근 크기와 위치를 표시합니다. 이 값은 창이 활성화되지 않았다는 점을 제외하고 <strong>SW_SHOWNORMAL</strong> 비슷합니다.</td>
</tr>
<tr>
<td><strong>SW_SHOW</strong><br>5</td>
<td>창을 활성화하고 현재 크기와 위치에 표시합니다.</td>
</tr>
<tr>
<td><strong>SW_MINIMIZE</strong><br>6</td>
<td>지정된 창을 최소화하고 Z 순서로 다음 최상위 창을 활성화합니다.</td>
</tr>
<tr>
<td><strong>SW_SHOWMINNOACTIVE</strong><br>7</td>
<td>창을 최소화된 창으로 표시합니다. 이 값은 창이 활성화되지 않은 경우를 제외하고 <strong>SW_SHOWMINIMIZED</strong> 비슷합니다.</td>
</tr>
<tr>
<td><strong>SW_SHOWNA</strong><br>8</td>
<td>창의 현재 크기와 위치를 표시합니다. 이 값은 창이 활성화되지 않았다는 점을 제외하고 <strong>SW_SHOW</strong> 비슷합니다.</td>
</tr>
<tr>
<td><strong>SW_RESTORE</strong><br>9</td>
<td>창을 활성화하고 표시합니다. 창이 최소화되거나 최대화되면 시스템은 창을 원래 크기와 위치로 복원합니다. 애플리케이션은 최소화된 창을 복원할 때 이 플래그를 지정해야 합니다.</td>
</tr>
<tr>
<td><strong>SW_SHOWDEFAULT</strong><br>10</td>
<td>애플리케이션을 시작한 프로그램에서 <a href="https://learn.microsoft.com/ko-kr/windows/win32/api/processthreadsapi/nf-processthreadsapi-createprocessa" data-linktype="absolute-path">CreateProcess</a> 함수에 전달된 <a href="https://learn.microsoft.com/ko-kr/windows/win32/api/processthreadsapi/ns-processthreadsapi-startupinfoa" data-linktype="absolute-path">STARTUPINFO</a> 구조체에 지정된 <strong>SW_</strong> 값을 기반으로 표시 상태를 설정합니다.</td>
</tr>
<tr>
<td><strong>SW_FORCEMINIMIZE</strong><br>11</td>
<td>창을 소유하는 스레드가 응답하지 않는 경우에도 창을 최소화합니다. 이 플래그는 다른 스레드에서 창을 최소화하는 경우에만 사용해야 합니다.</td>
</tr>
</tbody>
</table>

<br>

## 참조

<br>

1. [ShowWindow 함수(winuser.h)](https://learn.microsoft.com/ko-kr/windows/win32/api/winuser/nf-winuser-showwindow)