<h1 id="title">C# - Screen capture or Control capture</h1>

<h2 id="intro">Introduction</h2>

1. Screen의 PrimaryScreen을 통해 1번 모니터 정보를, Graphics의 CopyFromScreen을 이용해 화면 영역을 캡쳐할 수 있다.
2. Screen의 AllScreens 배열을 이용하면 특정 모니터의 정보를 가져올 수 있다. (여러 모니터를 동시에 사용하는 경우)
3. Control의 위치 정보를 통해 특정 컨트롤의 영역만 캡쳐할 수 있다.

<br><br>

<h2 id="code">Code</h2>

```csharp
using System.Drawing;
using System.Windows.Forms;

namespace ScreenCapture
{
    public static class ScreenCapture
    {
        // Control capture
        public static void CaptureControl(Control control)
        {
            Bitmap image = new Bitmap(control.Width, control.Height);
            using (Graphics g = Graphics.FromImage(image))
            {
                if (control.TopLevelControl != control) g.CopyFromScreen(control.Parent.PointToScreen(new Point(control.Left, control.Top)), new Point(0, 0), control.Size);  // Inside control
                else g.CopyFromScreen(new Point(control.Left, control.Top), new Point(0, 0), control.Size);  // Top level control (ex : Form)
            }
            SaveImage(image);
        }

        // Monitor capture
        // If need to capture specific monitor, see following code snippet.
        // var monitorInfo = Screen.AllScreens[0].Bounds;  // 1st monitor's information
        public static void CaptureScreen()
        {
            var monitorInfo = Screen.PrimaryScreen.Bounds;

            Bitmap image = new Bitmap(monitorInfo.Width, monitorInfo.Height);
            using (Graphics g = Graphics.FromImage(image))
            {
                g.CopyFromScreen(monitorInfo.Left, monitorInfo.Top, 0, 0, monitorInfo.Size);
            }
            SaveImage(image);
        }

        static void SaveImage(Bitmap image)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Save screenshot..";
            saveDialog.InitialDirectory = "C:\\";
            saveDialog.DefaultExt = "bmp";
            saveDialog.Filter = "bmp file|*.bmp;";
            saveDialog.OverwritePrompt = true;
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                image.Save(saveDialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
            }
        }
    }
}
```

<br><br>

<h2 id="example">Use example</h2>

```csharp
// Control capture
private void Control_Click(object sender, EventArgs e) => ScreenCapture.CaptureControl(Control);

// Monitor capture
private void ScreenCapture_Click(object sender, EventArgs e) => ScreenCapture.CaptureScreen();
```