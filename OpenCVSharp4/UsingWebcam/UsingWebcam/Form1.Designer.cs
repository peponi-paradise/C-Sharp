namespace UsingWebcam;

partial class Form1
{
    /// <summary>
    /// 필수 디자이너 변수입니다.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// 사용 중인 모든 리소스를 정리합니다.
    /// </summary>
    /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form 디자이너에서 생성한 코드

    /// <summary>
    /// 디자이너 지원에 필요한 메서드입니다. 
    /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
    /// </summary>
    private void InitializeComponent()
    {
        ControlPanel = new TableLayoutPanel();
        CamListBox = new GroupBox();
        WebcamList = new ListBox();
        WebcamStartStopPanel = new TableLayoutPanel();
        StopButton = new Button();
        StartButton = new Button();
        PropertyPanel = new TableLayoutPanel();
        SharpnessPanel = new TableLayoutPanel();
        SharpnessLabel = new Label();
        Sharpness = new TrackBar();
        GainGammaPanel = new TableLayoutPanel();
        Gamma = new TrackBar();
        GammaLabel = new Label();
        GainLabel = new Label();
        Gain = new TrackBar();
        HueSaturationPanel = new TableLayoutPanel();
        Saturation = new TrackBar();
        SaturationLabel = new Label();
        HueLabel = new Label();
        Hue = new TrackBar();
        ExposureFocusPanel = new TableLayoutPanel();
        Focus = new TrackBar();
        FocusLabel = new Label();
        ExposureLabel = new Label();
        Exposure = new TrackBar();
        BrightnessContrastPanel = new TableLayoutPanel();
        Contrast = new TrackBar();
        ContrastLabel = new Label();
        BrightnessLabel = new Label();
        Brightness = new TrackBar();
        FrameSizePanel = new TableLayoutPanel();
        FrameHeightLabel = new Label();
        FrameHeight = new NumericUpDown();
        FrameWidthLabel = new Label();
        FrameWidth = new NumericUpDown();
        ControlPanel.SuspendLayout();
        CamListBox.SuspendLayout();
        WebcamStartStopPanel.SuspendLayout();
        PropertyPanel.SuspendLayout();
        SharpnessPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)Sharpness).BeginInit();
        GainGammaPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)Gamma).BeginInit();
        ((System.ComponentModel.ISupportInitialize)Gain).BeginInit();
        HueSaturationPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)Saturation).BeginInit();
        ((System.ComponentModel.ISupportInitialize)Hue).BeginInit();
        ExposureFocusPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)Focus).BeginInit();
        ((System.ComponentModel.ISupportInitialize)Exposure).BeginInit();
        BrightnessContrastPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)Contrast).BeginInit();
        ((System.ComponentModel.ISupportInitialize)Brightness).BeginInit();
        FrameSizePanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)FrameHeight).BeginInit();
        ((System.ComponentModel.ISupportInitialize)FrameWidth).BeginInit();
        SuspendLayout();
        // 
        // ControlPanel
        // 
        ControlPanel.ColumnCount = 2;
        ControlPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
        ControlPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
        ControlPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
        ControlPanel.Controls.Add(CamListBox, 0, 0);
        ControlPanel.Controls.Add(WebcamStartStopPanel, 0, 1);
        ControlPanel.Controls.Add(PropertyPanel, 1, 0);
        ControlPanel.Dock = DockStyle.Fill;
        ControlPanel.Location = new Point(0, 0);
        ControlPanel.Margin = new Padding(0);
        ControlPanel.Name = "ControlPanel";
        ControlPanel.RowCount = 2;
        ControlPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        ControlPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        ControlPanel.Size = new Size(624, 441);
        ControlPanel.TabIndex = 1;
        // 
        // CamListBox
        // 
        CamListBox.Controls.Add(WebcamList);
        CamListBox.Dock = DockStyle.Fill;
        CamListBox.Location = new Point(1, 1);
        CamListBox.Margin = new Padding(1);
        CamListBox.Name = "CamListBox";
        CamListBox.Padding = new Padding(1);
        CamListBox.Size = new Size(122, 218);
        CamListBox.TabIndex = 4;
        CamListBox.TabStop = false;
        CamListBox.Text = "Webcams";
        // 
        // WebcamList
        // 
        WebcamList.Dock = DockStyle.Fill;
        WebcamList.FormattingEnabled = true;
        WebcamList.IntegralHeight = false;
        WebcamList.ItemHeight = 15;
        WebcamList.Location = new Point(1, 17);
        WebcamList.Name = "WebcamList";
        WebcamList.Size = new Size(120, 200);
        WebcamList.TabIndex = 0;
        // 
        // WebcamStartStopPanel
        // 
        WebcamStartStopPanel.ColumnCount = 2;
        WebcamStartStopPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        WebcamStartStopPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        WebcamStartStopPanel.Controls.Add(StopButton, 1, 0);
        WebcamStartStopPanel.Controls.Add(StartButton, 0, 0);
        WebcamStartStopPanel.Dock = DockStyle.Fill;
        WebcamStartStopPanel.Location = new Point(3, 223);
        WebcamStartStopPanel.Name = "WebcamStartStopPanel";
        WebcamStartStopPanel.RowCount = 1;
        WebcamStartStopPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        WebcamStartStopPanel.Size = new Size(118, 215);
        WebcamStartStopPanel.TabIndex = 5;
        // 
        // StopButton
        // 
        StopButton.Dock = DockStyle.Fill;
        StopButton.Location = new Point(60, 1);
        StopButton.Margin = new Padding(1);
        StopButton.Name = "StopButton";
        StopButton.Size = new Size(57, 213);
        StopButton.TabIndex = 3;
        StopButton.Text = "STOP";
        StopButton.UseVisualStyleBackColor = true;
        // 
        // StartButton
        // 
        StartButton.Dock = DockStyle.Fill;
        StartButton.Location = new Point(1, 1);
        StartButton.Margin = new Padding(1);
        StartButton.Name = "StartButton";
        StartButton.Size = new Size(57, 213);
        StartButton.TabIndex = 2;
        StartButton.Text = "START";
        StartButton.UseVisualStyleBackColor = true;
        // 
        // PropertyPanel
        // 
        PropertyPanel.ColumnCount = 4;
        PropertyPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        PropertyPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        PropertyPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        PropertyPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        PropertyPanel.Controls.Add(SharpnessPanel, 2, 2);
        PropertyPanel.Controls.Add(GainGammaPanel, 0, 2);
        PropertyPanel.Controls.Add(HueSaturationPanel, 2, 1);
        PropertyPanel.Controls.Add(ExposureFocusPanel, 2, 0);
        PropertyPanel.Controls.Add(BrightnessContrastPanel, 0, 1);
        PropertyPanel.Controls.Add(FrameSizePanel, 0, 0);
        PropertyPanel.Dock = DockStyle.Fill;
        PropertyPanel.Location = new Point(127, 3);
        PropertyPanel.Name = "PropertyPanel";
        PropertyPanel.RowCount = 3;
        ControlPanel.SetRowSpan(PropertyPanel, 2);
        PropertyPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333F));
        PropertyPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333F));
        PropertyPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333F));
        PropertyPanel.Size = new Size(494, 435);
        PropertyPanel.TabIndex = 6;
        // 
        // SharpnessPanel
        // 
        SharpnessPanel.ColumnCount = 2;
        PropertyPanel.SetColumnSpan(SharpnessPanel, 2);
        SharpnessPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        SharpnessPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        SharpnessPanel.Controls.Add(SharpnessLabel, 0, 0);
        SharpnessPanel.Controls.Add(Sharpness, 1, 0);
        SharpnessPanel.Dock = DockStyle.Fill;
        SharpnessPanel.Location = new Point(249, 293);
        SharpnessPanel.Name = "SharpnessPanel";
        SharpnessPanel.RowCount = 2;
        SharpnessPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        SharpnessPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        SharpnessPanel.Size = new Size(242, 139);
        SharpnessPanel.TabIndex = 5;
        // 
        // SharpnessLabel
        // 
        SharpnessLabel.AutoSize = true;
        SharpnessLabel.BorderStyle = BorderStyle.FixedSingle;
        SharpnessLabel.Dock = DockStyle.Fill;
        SharpnessLabel.Location = new Point(3, 0);
        SharpnessLabel.Name = "SharpnessLabel";
        SharpnessLabel.Size = new Size(115, 69);
        SharpnessLabel.TabIndex = 0;
        SharpnessLabel.Text = "Sharpness";
        SharpnessLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // Sharpness
        // 
        Sharpness.Dock = DockStyle.Fill;
        Sharpness.Location = new Point(124, 3);
        Sharpness.Maximum = 15;
        Sharpness.Name = "Sharpness";
        Sharpness.Size = new Size(115, 63);
        Sharpness.TabIndex = 3;
        Sharpness.TickFrequency = 5;
        Sharpness.Value = 1;
        // 
        // GainGammaPanel
        // 
        GainGammaPanel.ColumnCount = 2;
        PropertyPanel.SetColumnSpan(GainGammaPanel, 2);
        GainGammaPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        GainGammaPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        GainGammaPanel.Controls.Add(Gamma, 1, 1);
        GainGammaPanel.Controls.Add(GammaLabel, 0, 1);
        GainGammaPanel.Controls.Add(GainLabel, 0, 0);
        GainGammaPanel.Controls.Add(Gain, 1, 0);
        GainGammaPanel.Dock = DockStyle.Fill;
        GainGammaPanel.Location = new Point(3, 293);
        GainGammaPanel.Name = "GainGammaPanel";
        GainGammaPanel.RowCount = 2;
        GainGammaPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        GainGammaPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        GainGammaPanel.Size = new Size(240, 139);
        GainGammaPanel.TabIndex = 4;
        // 
        // Gamma
        // 
        Gamma.Dock = DockStyle.Fill;
        Gamma.Location = new Point(123, 72);
        Gamma.Maximum = 15;
        Gamma.Minimum = 1;
        Gamma.Name = "Gamma";
        Gamma.Size = new Size(114, 64);
        Gamma.TabIndex = 4;
        Gamma.TickFrequency = 5;
        Gamma.Value = 10;
        // 
        // GammaLabel
        // 
        GammaLabel.AutoSize = true;
        GammaLabel.BorderStyle = BorderStyle.FixedSingle;
        GammaLabel.Dock = DockStyle.Fill;
        GammaLabel.Location = new Point(3, 69);
        GammaLabel.Name = "GammaLabel";
        GammaLabel.Size = new Size(114, 70);
        GammaLabel.TabIndex = 2;
        GammaLabel.Text = "Gamma";
        GammaLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // GainLabel
        // 
        GainLabel.AutoSize = true;
        GainLabel.BorderStyle = BorderStyle.FixedSingle;
        GainLabel.Dock = DockStyle.Fill;
        GainLabel.Location = new Point(3, 0);
        GainLabel.Name = "GainLabel";
        GainLabel.Size = new Size(114, 69);
        GainLabel.TabIndex = 0;
        GainLabel.Text = "Gain";
        GainLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // Gain
        // 
        Gain.Dock = DockStyle.Fill;
        Gain.Location = new Point(123, 3);
        Gain.Maximum = 255;
        Gain.Minimum = -1;
        Gain.Name = "Gain";
        Gain.Size = new Size(114, 63);
        Gain.TabIndex = 3;
        Gain.TickFrequency = 5;
        // 
        // HueSaturationPanel
        // 
        HueSaturationPanel.ColumnCount = 2;
        PropertyPanel.SetColumnSpan(HueSaturationPanel, 2);
        HueSaturationPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        HueSaturationPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        HueSaturationPanel.Controls.Add(Saturation, 1, 1);
        HueSaturationPanel.Controls.Add(SaturationLabel, 0, 1);
        HueSaturationPanel.Controls.Add(HueLabel, 0, 0);
        HueSaturationPanel.Controls.Add(Hue, 1, 0);
        HueSaturationPanel.Dock = DockStyle.Fill;
        HueSaturationPanel.Location = new Point(249, 148);
        HueSaturationPanel.Name = "HueSaturationPanel";
        HueSaturationPanel.RowCount = 2;
        HueSaturationPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        HueSaturationPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        HueSaturationPanel.Size = new Size(242, 139);
        HueSaturationPanel.TabIndex = 3;
        // 
        // Saturation
        // 
        Saturation.Dock = DockStyle.Fill;
        Saturation.Location = new Point(124, 72);
        Saturation.Maximum = 255;
        Saturation.Name = "Saturation";
        Saturation.Size = new Size(115, 64);
        Saturation.TabIndex = 4;
        Saturation.TickFrequency = 5;
        Saturation.Value = 10;
        // 
        // SaturationLabel
        // 
        SaturationLabel.AutoSize = true;
        SaturationLabel.BorderStyle = BorderStyle.FixedSingle;
        SaturationLabel.Dock = DockStyle.Fill;
        SaturationLabel.Location = new Point(3, 69);
        SaturationLabel.Name = "SaturationLabel";
        SaturationLabel.Size = new Size(115, 70);
        SaturationLabel.TabIndex = 2;
        SaturationLabel.Text = "Saturation";
        SaturationLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // HueLabel
        // 
        HueLabel.AutoSize = true;
        HueLabel.BorderStyle = BorderStyle.FixedSingle;
        HueLabel.Dock = DockStyle.Fill;
        HueLabel.Location = new Point(3, 0);
        HueLabel.Name = "HueLabel";
        HueLabel.Size = new Size(115, 69);
        HueLabel.TabIndex = 0;
        HueLabel.Text = "Hue";
        HueLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // Hue
        // 
        Hue.Dock = DockStyle.Fill;
        Hue.Location = new Point(124, 3);
        Hue.Maximum = 179;
        Hue.Name = "Hue";
        Hue.Size = new Size(115, 63);
        Hue.TabIndex = 3;
        Hue.TickFrequency = 5;
        Hue.Value = 10;
        // 
        // ExposureFocusPanel
        // 
        ExposureFocusPanel.ColumnCount = 2;
        PropertyPanel.SetColumnSpan(ExposureFocusPanel, 2);
        ExposureFocusPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        ExposureFocusPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        ExposureFocusPanel.Controls.Add(Focus, 1, 1);
        ExposureFocusPanel.Controls.Add(FocusLabel, 0, 1);
        ExposureFocusPanel.Controls.Add(ExposureLabel, 0, 0);
        ExposureFocusPanel.Controls.Add(Exposure, 1, 0);
        ExposureFocusPanel.Dock = DockStyle.Fill;
        ExposureFocusPanel.Location = new Point(249, 3);
        ExposureFocusPanel.Name = "ExposureFocusPanel";
        ExposureFocusPanel.RowCount = 2;
        ExposureFocusPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        ExposureFocusPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        ExposureFocusPanel.Size = new Size(242, 139);
        ExposureFocusPanel.TabIndex = 2;
        // 
        // Focus
        // 
        Focus.Dock = DockStyle.Fill;
        Focus.Location = new Point(124, 72);
        Focus.Maximum = 255;
        Focus.Minimum = -1;
        Focus.Name = "Focus";
        Focus.Size = new Size(115, 64);
        Focus.TabIndex = 4;
        Focus.TickFrequency = 5;
        Focus.Value = 1;
        // 
        // FocusLabel
        // 
        FocusLabel.AutoSize = true;
        FocusLabel.BorderStyle = BorderStyle.FixedSingle;
        FocusLabel.Dock = DockStyle.Fill;
        FocusLabel.Location = new Point(3, 69);
        FocusLabel.Name = "FocusLabel";
        FocusLabel.Size = new Size(115, 70);
        FocusLabel.TabIndex = 2;
        FocusLabel.Text = "Focus";
        FocusLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // ExposureLabel
        // 
        ExposureLabel.AutoSize = true;
        ExposureLabel.BorderStyle = BorderStyle.FixedSingle;
        ExposureLabel.Dock = DockStyle.Fill;
        ExposureLabel.Location = new Point(3, 0);
        ExposureLabel.Name = "ExposureLabel";
        ExposureLabel.Size = new Size(115, 69);
        ExposureLabel.TabIndex = 0;
        ExposureLabel.Text = "Exposure";
        ExposureLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // Exposure
        // 
        Exposure.Dock = DockStyle.Fill;
        Exposure.Location = new Point(124, 3);
        Exposure.Maximum = -1;
        Exposure.Minimum = -13;
        Exposure.Name = "Exposure";
        Exposure.Size = new Size(115, 63);
        Exposure.TabIndex = 3;
        Exposure.TickFrequency = 5;
        Exposure.Value = -1;
        // 
        // BrightnessContrastPanel
        // 
        BrightnessContrastPanel.ColumnCount = 2;
        PropertyPanel.SetColumnSpan(BrightnessContrastPanel, 2);
        BrightnessContrastPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        BrightnessContrastPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        BrightnessContrastPanel.Controls.Add(Contrast, 1, 1);
        BrightnessContrastPanel.Controls.Add(ContrastLabel, 0, 1);
        BrightnessContrastPanel.Controls.Add(BrightnessLabel, 0, 0);
        BrightnessContrastPanel.Controls.Add(Brightness, 1, 0);
        BrightnessContrastPanel.Dock = DockStyle.Fill;
        BrightnessContrastPanel.Location = new Point(3, 148);
        BrightnessContrastPanel.Name = "BrightnessContrastPanel";
        BrightnessContrastPanel.RowCount = 2;
        BrightnessContrastPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        BrightnessContrastPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        BrightnessContrastPanel.Size = new Size(240, 139);
        BrightnessContrastPanel.TabIndex = 1;
        // 
        // Contrast
        // 
        Contrast.Dock = DockStyle.Fill;
        Contrast.Location = new Point(123, 72);
        Contrast.Maximum = 100;
        Contrast.Minimum = 1;
        Contrast.Name = "Contrast";
        Contrast.Size = new Size(114, 64);
        Contrast.TabIndex = 4;
        Contrast.TickFrequency = 5;
        Contrast.Value = 10;
        // 
        // ContrastLabel
        // 
        ContrastLabel.AutoSize = true;
        ContrastLabel.BorderStyle = BorderStyle.FixedSingle;
        ContrastLabel.Dock = DockStyle.Fill;
        ContrastLabel.Location = new Point(3, 69);
        ContrastLabel.Name = "ContrastLabel";
        ContrastLabel.Size = new Size(114, 70);
        ContrastLabel.TabIndex = 2;
        ContrastLabel.Text = "Contrast";
        ContrastLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // BrightnessLabel
        // 
        BrightnessLabel.AutoSize = true;
        BrightnessLabel.BorderStyle = BorderStyle.FixedSingle;
        BrightnessLabel.Dock = DockStyle.Fill;
        BrightnessLabel.Location = new Point(3, 0);
        BrightnessLabel.Name = "BrightnessLabel";
        BrightnessLabel.Size = new Size(114, 69);
        BrightnessLabel.TabIndex = 0;
        BrightnessLabel.Text = "Brightness";
        BrightnessLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // Brightness
        // 
        Brightness.Dock = DockStyle.Fill;
        Brightness.Location = new Point(123, 3);
        Brightness.Maximum = 100;
        Brightness.Minimum = 1;
        Brightness.Name = "Brightness";
        Brightness.Size = new Size(114, 63);
        Brightness.TabIndex = 3;
        Brightness.TickFrequency = 5;
        Brightness.Value = 10;
        // 
        // FrameSizePanel
        // 
        FrameSizePanel.ColumnCount = 2;
        PropertyPanel.SetColumnSpan(FrameSizePanel, 2);
        FrameSizePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        FrameSizePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        FrameSizePanel.Controls.Add(FrameHeightLabel, 0, 1);
        FrameSizePanel.Controls.Add(FrameHeight, 1, 1);
        FrameSizePanel.Controls.Add(FrameWidthLabel, 0, 0);
        FrameSizePanel.Controls.Add(FrameWidth, 1, 0);
        FrameSizePanel.Dock = DockStyle.Fill;
        FrameSizePanel.Location = new Point(3, 3);
        FrameSizePanel.Name = "FrameSizePanel";
        FrameSizePanel.RowCount = 2;
        FrameSizePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        FrameSizePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        FrameSizePanel.Size = new Size(240, 139);
        FrameSizePanel.TabIndex = 0;
        // 
        // FrameHeightLabel
        // 
        FrameHeightLabel.AutoSize = true;
        FrameHeightLabel.BorderStyle = BorderStyle.FixedSingle;
        FrameHeightLabel.Dock = DockStyle.Fill;
        FrameHeightLabel.Location = new Point(3, 69);
        FrameHeightLabel.Name = "FrameHeightLabel";
        FrameHeightLabel.Size = new Size(114, 70);
        FrameHeightLabel.TabIndex = 2;
        FrameHeightLabel.Text = "Frame Height";
        FrameHeightLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // FrameHeight
        // 
        FrameHeight.Dock = DockStyle.Fill;
        FrameHeight.Location = new Point(123, 72);
        FrameHeight.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        FrameHeight.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        FrameHeight.Name = "FrameHeight";
        FrameHeight.Size = new Size(114, 23);
        FrameHeight.TabIndex = 3;
        FrameHeight.Value = new decimal(new int[] { 768, 0, 0, 0 });
        // 
        // FrameWidthLabel
        // 
        FrameWidthLabel.AutoSize = true;
        FrameWidthLabel.BorderStyle = BorderStyle.FixedSingle;
        FrameWidthLabel.Dock = DockStyle.Fill;
        FrameWidthLabel.Location = new Point(3, 0);
        FrameWidthLabel.Name = "FrameWidthLabel";
        FrameWidthLabel.Size = new Size(114, 69);
        FrameWidthLabel.TabIndex = 0;
        FrameWidthLabel.Text = "Frame Width";
        FrameWidthLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // FrameWidth
        // 
        FrameWidth.Dock = DockStyle.Fill;
        FrameWidth.Location = new Point(123, 3);
        FrameWidth.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        FrameWidth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        FrameWidth.Name = "FrameWidth";
        FrameWidth.Size = new Size(114, 23);
        FrameWidth.TabIndex = 1;
        FrameWidth.Value = new decimal(new int[] { 1024, 0, 0, 0 });
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        ClientSize = new Size(624, 441);
        Controls.Add(ControlPanel);
        Name = "Form1";
        Text = "Form1";
        ControlPanel.ResumeLayout(false);
        CamListBox.ResumeLayout(false);
        WebcamStartStopPanel.ResumeLayout(false);
        PropertyPanel.ResumeLayout(false);
        SharpnessPanel.ResumeLayout(false);
        SharpnessPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)Sharpness).EndInit();
        GainGammaPanel.ResumeLayout(false);
        GainGammaPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)Gamma).EndInit();
        ((System.ComponentModel.ISupportInitialize)Gain).EndInit();
        HueSaturationPanel.ResumeLayout(false);
        HueSaturationPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)Saturation).EndInit();
        ((System.ComponentModel.ISupportInitialize)Hue).EndInit();
        ExposureFocusPanel.ResumeLayout(false);
        ExposureFocusPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)Focus).EndInit();
        ((System.ComponentModel.ISupportInitialize)Exposure).EndInit();
        BrightnessContrastPanel.ResumeLayout(false);
        BrightnessContrastPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)Contrast).EndInit();
        ((System.ComponentModel.ISupportInitialize)Brightness).EndInit();
        FrameSizePanel.ResumeLayout(false);
        FrameSizePanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)FrameHeight).EndInit();
        ((System.ComponentModel.ISupportInitialize)FrameWidth).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel ControlPanel;
    private System.Windows.Forms.GroupBox CamListBox;
    private System.Windows.Forms.ListBox WebcamList;
    private System.Windows.Forms.TableLayoutPanel WebcamStartStopPanel;
    private System.Windows.Forms.Button StopButton;
    private System.Windows.Forms.Button StartButton;
    private System.Windows.Forms.TableLayoutPanel PropertyPanel;
    private System.Windows.Forms.TableLayoutPanel FrameSizePanel;
    private System.Windows.Forms.Label FrameHeightLabel;
    private System.Windows.Forms.NumericUpDown FrameHeight;
    private System.Windows.Forms.Label FrameWidthLabel;
    private System.Windows.Forms.NumericUpDown FrameWidth;
    private System.Windows.Forms.TableLayoutPanel BrightnessContrastPanel;
    private System.Windows.Forms.TrackBar Contrast;
    private System.Windows.Forms.Label ContrastLabel;
    private System.Windows.Forms.Label BrightnessLabel;
    private System.Windows.Forms.TrackBar Brightness;
    private System.Windows.Forms.TableLayoutPanel ExposureFocusPanel;
    private System.Windows.Forms.TrackBar Focus;
    private System.Windows.Forms.Label FocusLabel;
    private System.Windows.Forms.Label ExposureLabel;
    private System.Windows.Forms.TrackBar Exposure;
    private System.Windows.Forms.TableLayoutPanel HueSaturationPanel;
    private System.Windows.Forms.TrackBar Saturation;
    private System.Windows.Forms.Label SaturationLabel;
    private System.Windows.Forms.Label HueLabel;
    private System.Windows.Forms.TrackBar Hue;
    private System.Windows.Forms.TableLayoutPanel GainGammaPanel;
    private System.Windows.Forms.TrackBar Gamma;
    private System.Windows.Forms.Label GammaLabel;
    private System.Windows.Forms.Label GainLabel;
    private System.Windows.Forms.TrackBar Gain;
    private System.Windows.Forms.TableLayoutPanel SharpnessPanel;
    private System.Windows.Forms.Label SharpnessLabel;
    private System.Windows.Forms.TrackBar Sharpness;
}
