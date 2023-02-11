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