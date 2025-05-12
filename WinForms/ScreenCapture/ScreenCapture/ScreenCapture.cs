using System.Drawing;
using System.Windows.Forms;

namespace ScreenCapture
{
    public static class ScreenCapture
    {
        public static void Control(Control control)
        {
            Bitmap image = new Bitmap(control.Width, control.Height);
            control.DrawToBitmap(image, control.ClientRectangle);

            image.SaveDialog();
        }

        public static void Monitor(int index)
        {
            var monitorInfo = Screen.AllScreens[index].Bounds;

            Bitmap image = new Bitmap(monitorInfo.Width, monitorInfo.Height);
            using (Graphics g = Graphics.FromImage(image))
            {
                g.CopyFromScreen(monitorInfo.Left, monitorInfo.Top, 0, 0, monitorInfo.Size);
            }

            image.SaveDialog();
        }
    }

    public static class BitmapExtension
    {
        public static void SaveDialog(this Bitmap image)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Title = "Save screenshot..",
                InitialDirectory = "C:\\",
                DefaultExt = "bmp",
                Filter = "bmp file|*.bmp;",
                OverwritePrompt = true
            };
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                image.Save(saveDialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
            }
        }
    }
}