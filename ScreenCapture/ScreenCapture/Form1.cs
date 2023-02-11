using System;
using System.Windows.Forms;

namespace ScreenCapture
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Control capture
        private void Control_Click(object sender, EventArgs e) => ScreenCapture.CaptureControl(Control);

        // Monitor capture
        private void ScreenCapture_Click(object sender, EventArgs e) => ScreenCapture.CaptureScreen();
    }
}