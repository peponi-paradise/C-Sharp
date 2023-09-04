
namespace ATIK
{
    partial class DevChartControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chart = new DevExpress.XtraCharts.ChartControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_Blank = new System.Windows.Forms.Button();
            this.button_YMinus = new System.Windows.Forms.Button();
            this.button_YPlus = new System.Windows.Forms.Button();
            this.button_Reset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart
            // 
            this.chart.AppearanceNameSerializable = "Pastel Kit";
            this.chart.BorderOptions.Color = System.Drawing.Color.Transparent;
            this.chart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart.Location = new System.Drawing.Point(0, 0);
            this.chart.Name = "chart";
            this.chart.PaletteName = "Pastel Kit";
            this.chart.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chart.Size = new System.Drawing.Size(500, 500);
            this.chart.TabIndex = 0;
            this.chart.UseDirectXPaint = true;
            this.chart.Zoom += new DevExpress.XtraCharts.ChartZoomEventHandler(this.chart_Zoom);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_Blank);
            this.panel1.Controls.Add(this.button_YMinus);
            this.panel1.Controls.Add(this.button_YPlus);
            this.panel1.Controls.Add(this.button_Reset);
            this.panel1.Controls.Add(this.chart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 500);
            this.panel1.TabIndex = 1;
            // 
            // button_Blank
            // 
            this.button_Blank.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Blank.Location = new System.Drawing.Point(297, 3);
            this.button_Blank.Name = "button_Blank";
            this.button_Blank.Size = new System.Drawing.Size(200, 25);
            this.button_Blank.TabIndex = 2;
            this.button_Blank.Text = "Blank";
            this.button_Blank.UseVisualStyleBackColor = true;
            this.button_Blank.Visible = false;
            // 
            // button_YMinus
            // 
            this.button_YMinus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_YMinus.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_YMinus.Location = new System.Drawing.Point(185, 3);
            this.button_YMinus.Name = "button_YMinus";
            this.button_YMinus.Size = new System.Drawing.Size(50, 30);
            this.button_YMinus.TabIndex = 1;
            this.button_YMinus.Text = "Y-";
            this.button_YMinus.UseVisualStyleBackColor = true;
            this.button_YMinus.Visible = false;
            this.button_YMinus.Click += new System.EventHandler(this.button_YMinus_Click);
            // 
            // button_YPlus
            // 
            this.button_YPlus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_YPlus.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_YPlus.Location = new System.Drawing.Point(129, 3);
            this.button_YPlus.Name = "button_YPlus";
            this.button_YPlus.Size = new System.Drawing.Size(50, 30);
            this.button_YPlus.TabIndex = 1;
            this.button_YPlus.Text = "Y+";
            this.button_YPlus.UseVisualStyleBackColor = true;
            this.button_YPlus.Visible = false;
            this.button_YPlus.Click += new System.EventHandler(this.button_YPlus_Click);
            // 
            // button_Reset
            // 
            this.button_Reset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Reset.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Reset.Location = new System.Drawing.Point(241, 3);
            this.button_Reset.Name = "button_Reset";
            this.button_Reset.Size = new System.Drawing.Size(50, 30);
            this.button_Reset.TabIndex = 1;
            this.button_Reset.Text = "-";
            this.button_Reset.UseVisualStyleBackColor = true;
            this.button_Reset.Visible = false;
            this.button_Reset.Click += new System.EventHandler(this.button_Reset_Click);
            // 
            // DevChartControl
            // 
            this.Appearance.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel1);
            this.Name = "DevChartControl";
            this.Size = new System.Drawing.Size(500, 500);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraCharts.ChartControl chart;
        private System.Windows.Forms.Button button_Reset;
        private System.Windows.Forms.Button button_YMinus;
        private System.Windows.Forms.Button button_YPlus;
        private System.Windows.Forms.Button button_Blank;
    }
}
