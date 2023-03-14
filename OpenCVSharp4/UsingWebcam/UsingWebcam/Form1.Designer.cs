namespace UsingWebcam
{
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
            this.ControlPanel = new System.Windows.Forms.TableLayoutPanel();
            this.CamListBox = new System.Windows.Forms.GroupBox();
            this.WebcamList = new System.Windows.Forms.ListBox();
            this.WebcamStartStopPanel = new System.Windows.Forms.TableLayoutPanel();
            this.StopButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.PropertyPanel = new System.Windows.Forms.TableLayoutPanel();
            this.SharpnessPanel = new System.Windows.Forms.TableLayoutPanel();
            this.SharpnessLabel = new System.Windows.Forms.Label();
            this.Sharpness = new System.Windows.Forms.TrackBar();
            this.GainGammaPanel = new System.Windows.Forms.TableLayoutPanel();
            this.Gamma = new System.Windows.Forms.TrackBar();
            this.GammaLabel = new System.Windows.Forms.Label();
            this.GainLabel = new System.Windows.Forms.Label();
            this.Gain = new System.Windows.Forms.TrackBar();
            this.HueSaturationPanel = new System.Windows.Forms.TableLayoutPanel();
            this.Saturation = new System.Windows.Forms.TrackBar();
            this.SaturationLabel = new System.Windows.Forms.Label();
            this.HueLabel = new System.Windows.Forms.Label();
            this.Hue = new System.Windows.Forms.TrackBar();
            this.ExposureFocusPanel = new System.Windows.Forms.TableLayoutPanel();
            this.Focus = new System.Windows.Forms.TrackBar();
            this.FocusLabel = new System.Windows.Forms.Label();
            this.ExposureLabel = new System.Windows.Forms.Label();
            this.Exposure = new System.Windows.Forms.TrackBar();
            this.BrightnessContrastPanel = new System.Windows.Forms.TableLayoutPanel();
            this.Contrast = new System.Windows.Forms.TrackBar();
            this.ContrastLabel = new System.Windows.Forms.Label();
            this.BrightnessLabel = new System.Windows.Forms.Label();
            this.Brightness = new System.Windows.Forms.TrackBar();
            this.FrameSizePanel = new System.Windows.Forms.TableLayoutPanel();
            this.FrameHeightLabel = new System.Windows.Forms.Label();
            this.FrameHeight = new System.Windows.Forms.NumericUpDown();
            this.FrameWidthLabel = new System.Windows.Forms.Label();
            this.FrameWidth = new System.Windows.Forms.NumericUpDown();
            this.ControlPanel.SuspendLayout();
            this.CamListBox.SuspendLayout();
            this.WebcamStartStopPanel.SuspendLayout();
            this.PropertyPanel.SuspendLayout();
            this.SharpnessPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sharpness)).BeginInit();
            this.GainGammaPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gamma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gain)).BeginInit();
            this.HueSaturationPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Saturation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hue)).BeginInit();
            this.ExposureFocusPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Focus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Exposure)).BeginInit();
            this.BrightnessContrastPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Contrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Brightness)).BeginInit();
            this.FrameSizePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FrameHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrameWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // ControlPanel
            // 
            this.ControlPanel.ColumnCount = 2;
            this.ControlPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.ControlPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.ControlPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.ControlPanel.Controls.Add(this.CamListBox, 0, 0);
            this.ControlPanel.Controls.Add(this.WebcamStartStopPanel, 0, 1);
            this.ControlPanel.Controls.Add(this.PropertyPanel, 1, 0);
            this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ControlPanel.Location = new System.Drawing.Point(0, 0);
            this.ControlPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.RowCount = 2;
            this.ControlPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ControlPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ControlPanel.Size = new System.Drawing.Size(624, 441);
            this.ControlPanel.TabIndex = 1;
            // 
            // CamListBox
            // 
            this.CamListBox.Controls.Add(this.WebcamList);
            this.CamListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CamListBox.Location = new System.Drawing.Point(1, 1);
            this.CamListBox.Margin = new System.Windows.Forms.Padding(1);
            this.CamListBox.Name = "CamListBox";
            this.CamListBox.Padding = new System.Windows.Forms.Padding(1);
            this.CamListBox.Size = new System.Drawing.Size(122, 218);
            this.CamListBox.TabIndex = 4;
            this.CamListBox.TabStop = false;
            this.CamListBox.Text = "Webcams";
            // 
            // WebcamList
            // 
            this.WebcamList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebcamList.FormattingEnabled = true;
            this.WebcamList.IntegralHeight = false;
            this.WebcamList.ItemHeight = 12;
            this.WebcamList.Location = new System.Drawing.Point(1, 15);
            this.WebcamList.Name = "WebcamList";
            this.WebcamList.Size = new System.Drawing.Size(120, 202);
            this.WebcamList.TabIndex = 0;
            // 
            // WebcamStartStopPanel
            // 
            this.WebcamStartStopPanel.ColumnCount = 2;
            this.WebcamStartStopPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.WebcamStartStopPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.WebcamStartStopPanel.Controls.Add(this.StopButton, 1, 0);
            this.WebcamStartStopPanel.Controls.Add(this.StartButton, 0, 0);
            this.WebcamStartStopPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebcamStartStopPanel.Location = new System.Drawing.Point(3, 223);
            this.WebcamStartStopPanel.Name = "WebcamStartStopPanel";
            this.WebcamStartStopPanel.RowCount = 1;
            this.WebcamStartStopPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.WebcamStartStopPanel.Size = new System.Drawing.Size(118, 215);
            this.WebcamStartStopPanel.TabIndex = 5;
            // 
            // StopButton
            // 
            this.StopButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StopButton.Location = new System.Drawing.Point(60, 1);
            this.StopButton.Margin = new System.Windows.Forms.Padding(1);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(57, 213);
            this.StopButton.TabIndex = 3;
            this.StopButton.Text = "STOP";
            this.StopButton.UseVisualStyleBackColor = true;
            // 
            // StartButton
            // 
            this.StartButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StartButton.Location = new System.Drawing.Point(1, 1);
            this.StartButton.Margin = new System.Windows.Forms.Padding(1);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(57, 213);
            this.StartButton.TabIndex = 2;
            this.StartButton.Text = "START";
            this.StartButton.UseVisualStyleBackColor = true;
            // 
            // PropertyPanel
            // 
            this.PropertyPanel.ColumnCount = 4;
            this.PropertyPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.PropertyPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.PropertyPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.PropertyPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.PropertyPanel.Controls.Add(this.SharpnessPanel, 2, 2);
            this.PropertyPanel.Controls.Add(this.GainGammaPanel, 0, 2);
            this.PropertyPanel.Controls.Add(this.HueSaturationPanel, 2, 1);
            this.PropertyPanel.Controls.Add(this.ExposureFocusPanel, 2, 0);
            this.PropertyPanel.Controls.Add(this.BrightnessContrastPanel, 0, 1);
            this.PropertyPanel.Controls.Add(this.FrameSizePanel, 0, 0);
            this.PropertyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PropertyPanel.Location = new System.Drawing.Point(127, 3);
            this.PropertyPanel.Name = "PropertyPanel";
            this.PropertyPanel.RowCount = 3;
            this.ControlPanel.SetRowSpan(this.PropertyPanel, 2);
            this.PropertyPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.PropertyPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.PropertyPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.PropertyPanel.Size = new System.Drawing.Size(494, 435);
            this.PropertyPanel.TabIndex = 6;
            // 
            // SharpnessPanel
            // 
            this.SharpnessPanel.ColumnCount = 2;
            this.PropertyPanel.SetColumnSpan(this.SharpnessPanel, 2);
            this.SharpnessPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.SharpnessPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.SharpnessPanel.Controls.Add(this.SharpnessLabel, 0, 0);
            this.SharpnessPanel.Controls.Add(this.Sharpness, 1, 0);
            this.SharpnessPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SharpnessPanel.Location = new System.Drawing.Point(249, 293);
            this.SharpnessPanel.Name = "SharpnessPanel";
            this.SharpnessPanel.RowCount = 2;
            this.SharpnessPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.SharpnessPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.SharpnessPanel.Size = new System.Drawing.Size(242, 139);
            this.SharpnessPanel.TabIndex = 5;
            // 
            // SharpnessLabel
            // 
            this.SharpnessLabel.AutoSize = true;
            this.SharpnessLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SharpnessLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SharpnessLabel.Location = new System.Drawing.Point(3, 0);
            this.SharpnessLabel.Name = "SharpnessLabel";
            this.SharpnessLabel.Size = new System.Drawing.Size(115, 69);
            this.SharpnessLabel.TabIndex = 0;
            this.SharpnessLabel.Text = "Sharpness";
            this.SharpnessLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Sharpness
            // 
            this.Sharpness.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Sharpness.Location = new System.Drawing.Point(124, 3);
            this.Sharpness.Maximum = 15;
            this.Sharpness.Name = "Sharpness";
            this.Sharpness.Size = new System.Drawing.Size(115, 63);
            this.Sharpness.TabIndex = 3;
            this.Sharpness.TickFrequency = 5;
            this.Sharpness.Value = 1;
            // 
            // GainGammaPanel
            // 
            this.GainGammaPanel.ColumnCount = 2;
            this.PropertyPanel.SetColumnSpan(this.GainGammaPanel, 2);
            this.GainGammaPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.GainGammaPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.GainGammaPanel.Controls.Add(this.Gamma, 1, 1);
            this.GainGammaPanel.Controls.Add(this.GammaLabel, 0, 1);
            this.GainGammaPanel.Controls.Add(this.GainLabel, 0, 0);
            this.GainGammaPanel.Controls.Add(this.Gain, 1, 0);
            this.GainGammaPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GainGammaPanel.Location = new System.Drawing.Point(3, 293);
            this.GainGammaPanel.Name = "GainGammaPanel";
            this.GainGammaPanel.RowCount = 2;
            this.GainGammaPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.GainGammaPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.GainGammaPanel.Size = new System.Drawing.Size(240, 139);
            this.GainGammaPanel.TabIndex = 4;
            // 
            // Gamma
            // 
            this.Gamma.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Gamma.Location = new System.Drawing.Point(123, 72);
            this.Gamma.Maximum = 15;
            this.Gamma.Minimum = 1;
            this.Gamma.Name = "Gamma";
            this.Gamma.Size = new System.Drawing.Size(114, 64);
            this.Gamma.TabIndex = 4;
            this.Gamma.TickFrequency = 5;
            this.Gamma.Value = 10;
            // 
            // GammaLabel
            // 
            this.GammaLabel.AutoSize = true;
            this.GammaLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GammaLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GammaLabel.Location = new System.Drawing.Point(3, 69);
            this.GammaLabel.Name = "GammaLabel";
            this.GammaLabel.Size = new System.Drawing.Size(114, 70);
            this.GammaLabel.TabIndex = 2;
            this.GammaLabel.Text = "Gamma";
            this.GammaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GainLabel
            // 
            this.GainLabel.AutoSize = true;
            this.GainLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GainLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GainLabel.Location = new System.Drawing.Point(3, 0);
            this.GainLabel.Name = "GainLabel";
            this.GainLabel.Size = new System.Drawing.Size(114, 69);
            this.GainLabel.TabIndex = 0;
            this.GainLabel.Text = "Gain";
            this.GainLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Gain
            // 
            this.Gain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Gain.Location = new System.Drawing.Point(123, 3);
            this.Gain.Maximum = 255;
            this.Gain.Minimum = -1;
            this.Gain.Name = "Gain";
            this.Gain.Size = new System.Drawing.Size(114, 63);
            this.Gain.TabIndex = 3;
            this.Gain.TickFrequency = 5;
            // 
            // HueSaturationPanel
            // 
            this.HueSaturationPanel.ColumnCount = 2;
            this.PropertyPanel.SetColumnSpan(this.HueSaturationPanel, 2);
            this.HueSaturationPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.HueSaturationPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.HueSaturationPanel.Controls.Add(this.Saturation, 1, 1);
            this.HueSaturationPanel.Controls.Add(this.SaturationLabel, 0, 1);
            this.HueSaturationPanel.Controls.Add(this.HueLabel, 0, 0);
            this.HueSaturationPanel.Controls.Add(this.Hue, 1, 0);
            this.HueSaturationPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HueSaturationPanel.Location = new System.Drawing.Point(249, 148);
            this.HueSaturationPanel.Name = "HueSaturationPanel";
            this.HueSaturationPanel.RowCount = 2;
            this.HueSaturationPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.HueSaturationPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.HueSaturationPanel.Size = new System.Drawing.Size(242, 139);
            this.HueSaturationPanel.TabIndex = 3;
            // 
            // Saturation
            // 
            this.Saturation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Saturation.Location = new System.Drawing.Point(124, 72);
            this.Saturation.Maximum = 255;
            this.Saturation.Name = "Saturation";
            this.Saturation.Size = new System.Drawing.Size(115, 64);
            this.Saturation.TabIndex = 4;
            this.Saturation.TickFrequency = 5;
            this.Saturation.Value = 10;
            // 
            // SaturationLabel
            // 
            this.SaturationLabel.AutoSize = true;
            this.SaturationLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SaturationLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SaturationLabel.Location = new System.Drawing.Point(3, 69);
            this.SaturationLabel.Name = "SaturationLabel";
            this.SaturationLabel.Size = new System.Drawing.Size(115, 70);
            this.SaturationLabel.TabIndex = 2;
            this.SaturationLabel.Text = "Saturation";
            this.SaturationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HueLabel
            // 
            this.HueLabel.AutoSize = true;
            this.HueLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HueLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HueLabel.Location = new System.Drawing.Point(3, 0);
            this.HueLabel.Name = "HueLabel";
            this.HueLabel.Size = new System.Drawing.Size(115, 69);
            this.HueLabel.TabIndex = 0;
            this.HueLabel.Text = "Hue";
            this.HueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Hue
            // 
            this.Hue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Hue.Location = new System.Drawing.Point(124, 3);
            this.Hue.Maximum = 179;
            this.Hue.Name = "Hue";
            this.Hue.Size = new System.Drawing.Size(115, 63);
            this.Hue.TabIndex = 3;
            this.Hue.TickFrequency = 5;
            this.Hue.Value = 10;
            // 
            // ExposureFocusPanel
            // 
            this.ExposureFocusPanel.ColumnCount = 2;
            this.PropertyPanel.SetColumnSpan(this.ExposureFocusPanel, 2);
            this.ExposureFocusPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ExposureFocusPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ExposureFocusPanel.Controls.Add(this.Focus, 1, 1);
            this.ExposureFocusPanel.Controls.Add(this.FocusLabel, 0, 1);
            this.ExposureFocusPanel.Controls.Add(this.ExposureLabel, 0, 0);
            this.ExposureFocusPanel.Controls.Add(this.Exposure, 1, 0);
            this.ExposureFocusPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExposureFocusPanel.Location = new System.Drawing.Point(249, 3);
            this.ExposureFocusPanel.Name = "ExposureFocusPanel";
            this.ExposureFocusPanel.RowCount = 2;
            this.ExposureFocusPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ExposureFocusPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ExposureFocusPanel.Size = new System.Drawing.Size(242, 139);
            this.ExposureFocusPanel.TabIndex = 2;
            // 
            // Focus
            // 
            this.Focus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Focus.Location = new System.Drawing.Point(124, 72);
            this.Focus.Maximum = 255;
            this.Focus.Minimum = -1;
            this.Focus.Name = "Focus";
            this.Focus.Size = new System.Drawing.Size(115, 64);
            this.Focus.TabIndex = 4;
            this.Focus.TickFrequency = 5;
            this.Focus.Value = 1;
            // 
            // FocusLabel
            // 
            this.FocusLabel.AutoSize = true;
            this.FocusLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FocusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FocusLabel.Location = new System.Drawing.Point(3, 69);
            this.FocusLabel.Name = "FocusLabel";
            this.FocusLabel.Size = new System.Drawing.Size(115, 70);
            this.FocusLabel.TabIndex = 2;
            this.FocusLabel.Text = "Focus";
            this.FocusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ExposureLabel
            // 
            this.ExposureLabel.AutoSize = true;
            this.ExposureLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ExposureLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExposureLabel.Location = new System.Drawing.Point(3, 0);
            this.ExposureLabel.Name = "ExposureLabel";
            this.ExposureLabel.Size = new System.Drawing.Size(115, 69);
            this.ExposureLabel.TabIndex = 0;
            this.ExposureLabel.Text = "Exposure";
            this.ExposureLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Exposure
            // 
            this.Exposure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Exposure.Location = new System.Drawing.Point(124, 3);
            this.Exposure.Maximum = -1;
            this.Exposure.Minimum = -13;
            this.Exposure.Name = "Exposure";
            this.Exposure.Size = new System.Drawing.Size(115, 63);
            this.Exposure.TabIndex = 3;
            this.Exposure.TickFrequency = 5;
            this.Exposure.Value = -1;
            // 
            // BrightnessContrastPanel
            // 
            this.BrightnessContrastPanel.ColumnCount = 2;
            this.PropertyPanel.SetColumnSpan(this.BrightnessContrastPanel, 2);
            this.BrightnessContrastPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.BrightnessContrastPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.BrightnessContrastPanel.Controls.Add(this.Contrast, 1, 1);
            this.BrightnessContrastPanel.Controls.Add(this.ContrastLabel, 0, 1);
            this.BrightnessContrastPanel.Controls.Add(this.BrightnessLabel, 0, 0);
            this.BrightnessContrastPanel.Controls.Add(this.Brightness, 1, 0);
            this.BrightnessContrastPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BrightnessContrastPanel.Location = new System.Drawing.Point(3, 148);
            this.BrightnessContrastPanel.Name = "BrightnessContrastPanel";
            this.BrightnessContrastPanel.RowCount = 2;
            this.BrightnessContrastPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.BrightnessContrastPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.BrightnessContrastPanel.Size = new System.Drawing.Size(240, 139);
            this.BrightnessContrastPanel.TabIndex = 1;
            // 
            // Contrast
            // 
            this.Contrast.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Contrast.Location = new System.Drawing.Point(123, 72);
            this.Contrast.Maximum = 100;
            this.Contrast.Minimum = 1;
            this.Contrast.Name = "Contrast";
            this.Contrast.Size = new System.Drawing.Size(114, 64);
            this.Contrast.TabIndex = 4;
            this.Contrast.TickFrequency = 5;
            this.Contrast.Value = 10;
            // 
            // ContrastLabel
            // 
            this.ContrastLabel.AutoSize = true;
            this.ContrastLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContrastLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContrastLabel.Location = new System.Drawing.Point(3, 69);
            this.ContrastLabel.Name = "ContrastLabel";
            this.ContrastLabel.Size = new System.Drawing.Size(114, 70);
            this.ContrastLabel.TabIndex = 2;
            this.ContrastLabel.Text = "Contrast";
            this.ContrastLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BrightnessLabel
            // 
            this.BrightnessLabel.AutoSize = true;
            this.BrightnessLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BrightnessLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BrightnessLabel.Location = new System.Drawing.Point(3, 0);
            this.BrightnessLabel.Name = "BrightnessLabel";
            this.BrightnessLabel.Size = new System.Drawing.Size(114, 69);
            this.BrightnessLabel.TabIndex = 0;
            this.BrightnessLabel.Text = "Brightness";
            this.BrightnessLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Brightness
            // 
            this.Brightness.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Brightness.Location = new System.Drawing.Point(123, 3);
            this.Brightness.Maximum = 100;
            this.Brightness.Minimum = 1;
            this.Brightness.Name = "Brightness";
            this.Brightness.Size = new System.Drawing.Size(114, 63);
            this.Brightness.TabIndex = 3;
            this.Brightness.TickFrequency = 5;
            this.Brightness.Value = 10;
            // 
            // FrameSizePanel
            // 
            this.FrameSizePanel.ColumnCount = 2;
            this.PropertyPanel.SetColumnSpan(this.FrameSizePanel, 2);
            this.FrameSizePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.FrameSizePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.FrameSizePanel.Controls.Add(this.FrameHeightLabel, 0, 1);
            this.FrameSizePanel.Controls.Add(this.FrameHeight, 1, 1);
            this.FrameSizePanel.Controls.Add(this.FrameWidthLabel, 0, 0);
            this.FrameSizePanel.Controls.Add(this.FrameWidth, 1, 0);
            this.FrameSizePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FrameSizePanel.Location = new System.Drawing.Point(3, 3);
            this.FrameSizePanel.Name = "FrameSizePanel";
            this.FrameSizePanel.RowCount = 2;
            this.FrameSizePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.FrameSizePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.FrameSizePanel.Size = new System.Drawing.Size(240, 139);
            this.FrameSizePanel.TabIndex = 0;
            // 
            // FrameHeightLabel
            // 
            this.FrameHeightLabel.AutoSize = true;
            this.FrameHeightLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FrameHeightLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FrameHeightLabel.Location = new System.Drawing.Point(3, 69);
            this.FrameHeightLabel.Name = "FrameHeightLabel";
            this.FrameHeightLabel.Size = new System.Drawing.Size(114, 70);
            this.FrameHeightLabel.TabIndex = 2;
            this.FrameHeightLabel.Text = "Frame Height";
            this.FrameHeightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrameHeight
            // 
            this.FrameHeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FrameHeight.Location = new System.Drawing.Point(123, 72);
            this.FrameHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.FrameHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FrameHeight.Name = "FrameHeight";
            this.FrameHeight.Size = new System.Drawing.Size(114, 21);
            this.FrameHeight.TabIndex = 3;
            this.FrameHeight.Value = new decimal(new int[] {
            768,
            0,
            0,
            0});
            // 
            // FrameWidthLabel
            // 
            this.FrameWidthLabel.AutoSize = true;
            this.FrameWidthLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FrameWidthLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FrameWidthLabel.Location = new System.Drawing.Point(3, 0);
            this.FrameWidthLabel.Name = "FrameWidthLabel";
            this.FrameWidthLabel.Size = new System.Drawing.Size(114, 69);
            this.FrameWidthLabel.TabIndex = 0;
            this.FrameWidthLabel.Text = "Frame Width";
            this.FrameWidthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrameWidth
            // 
            this.FrameWidth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FrameWidth.Location = new System.Drawing.Point(123, 3);
            this.FrameWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.FrameWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FrameWidth.Name = "FrameWidth";
            this.FrameWidth.Size = new System.Drawing.Size(114, 21);
            this.FrameWidth.TabIndex = 1;
            this.FrameWidth.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.ControlPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ControlPanel.ResumeLayout(false);
            this.CamListBox.ResumeLayout(false);
            this.WebcamStartStopPanel.ResumeLayout(false);
            this.PropertyPanel.ResumeLayout(false);
            this.SharpnessPanel.ResumeLayout(false);
            this.SharpnessPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sharpness)).EndInit();
            this.GainGammaPanel.ResumeLayout(false);
            this.GainGammaPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gamma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gain)).EndInit();
            this.HueSaturationPanel.ResumeLayout(false);
            this.HueSaturationPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Saturation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hue)).EndInit();
            this.ExposureFocusPanel.ResumeLayout(false);
            this.ExposureFocusPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Focus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Exposure)).EndInit();
            this.BrightnessContrastPanel.ResumeLayout(false);
            this.BrightnessContrastPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Contrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Brightness)).EndInit();
            this.FrameSizePanel.ResumeLayout(false);
            this.FrameSizePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FrameHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrameWidth)).EndInit();
            this.ResumeLayout(false);

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
}

