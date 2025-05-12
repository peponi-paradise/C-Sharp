using OpenCvSharp;

namespace RenderingText;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        var load = new Button()
        {
            Text = "LOAD",
            Dock = DockStyle.Fill,
        };
        load.Click += Load_Click;

        this.Controls.Add(load);
    }

    private void Load_Click(object? sender, EventArgs e)
    {
        OpenFileDialog dialog = new()
        {
            Filter = "All files|*.*",
            InitialDirectory = $@"C:\",
            CheckPathExists = true,
            CheckFileExists = true
        };
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            // Image load
            using var image = Cv2.ImRead(dialog.FileName);

            Cv2.PutText(image, dialog.SafeFileName, new(10, 70), HersheyFonts.HersheySimplex, 3, Scalar.MediumSeaGreen);

            Cv2.ImShow(dialog.FileName, image);
        }
    }
}