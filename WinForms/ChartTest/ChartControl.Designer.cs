
namespace ATIK
{
    partial class ChartControl
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.comboBox_ChartType = new System.Windows.Forms.ComboBox();
            this.button_Clear = new System.Windows.Forms.Button();
            this.numeric_Max = new System.Windows.Forms.NumericUpDown();
            this.numeric_Min = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_Max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_Min)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.chart);
            this.panel1.Controls.Add(this.comboBox_ChartType);
            this.panel1.Controls.Add(this.button_Clear);
            this.panel1.Controls.Add(this.numeric_Max);
            this.panel1.Controls.Add(this.numeric_Min);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 371);
            this.panel1.TabIndex = 0;
            // 
            // chart
            // 
            chartArea1.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea1);
            this.chart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(0, 0);
            this.chart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chart.Name = "chart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.PointAndFigure;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.YValuesPerPoint = 2;
            this.chart.Series.Add(series1);
            this.chart.Size = new System.Drawing.Size(560, 371);
            this.chart.TabIndex = 0;
            this.chart.Text = "chart1";
            // 
            // comboBox_ChartType
            // 
            this.comboBox_ChartType.FormattingEnabled = true;
            this.comboBox_ChartType.Items.AddRange(new object[] {
            "Line",
            "Column",
            "Point"});
            this.comboBox_ChartType.Location = new System.Drawing.Point(275, 420);
            this.comboBox_ChartType.Name = "comboBox_ChartType";
            this.comboBox_ChartType.Size = new System.Drawing.Size(85, 20);
            this.comboBox_ChartType.TabIndex = 12;
            this.comboBox_ChartType.Visible = false;
            this.comboBox_ChartType.SelectedIndexChanged += new System.EventHandler(this.comboBox_ChartType_SelectedIndexChanged);
            // 
            // button_Clear
            // 
            this.button_Clear.Location = new System.Drawing.Point(591, 411);
            this.button_Clear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(112, 33);
            this.button_Clear.TabIndex = 11;
            this.button_Clear.Text = "Clear";
            this.button_Clear.UseVisualStyleBackColor = true;
            this.button_Clear.Visible = false;
            this.button_Clear.Click += new System.EventHandler(this.button_Clear_Click);
            // 
            // numeric_Max
            // 
            this.numeric_Max.DecimalPlaces = 2;
            this.numeric_Max.Location = new System.Drawing.Point(146, 420);
            this.numeric_Max.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numeric_Max.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numeric_Max.Minimum = new decimal(new int[] {
            100000000,
            0,
            0,
            -2147483648});
            this.numeric_Max.Name = "numeric_Max";
            this.numeric_Max.Size = new System.Drawing.Size(88, 21);
            this.numeric_Max.TabIndex = 9;
            this.numeric_Max.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numeric_Max.Visible = false;
            this.numeric_Max.ValueChanged += new System.EventHandler(this.numeric_Max_ValueChanged);
            // 
            // numeric_Min
            // 
            this.numeric_Min.DecimalPlaces = 2;
            this.numeric_Min.Location = new System.Drawing.Point(3, 420);
            this.numeric_Min.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numeric_Min.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numeric_Min.Minimum = new decimal(new int[] {
            100000000,
            0,
            0,
            -2147483648});
            this.numeric_Min.Name = "numeric_Min";
            this.numeric_Min.Size = new System.Drawing.Size(88, 21);
            this.numeric_Min.TabIndex = 10;
            this.numeric_Min.Visible = false;
            this.numeric_Min.ValueChanged += new System.EventHandler(this.numeric_Min_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(239, 422);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "Max";
            this.label5.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(95, 422);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "Min";
            this.label4.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(273, 406);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "Chart Type";
            this.label1.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 406);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "Scale";
            this.label3.Visible = false;
            // 
            // ChartControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ChartControl";
            this.Size = new System.Drawing.Size(560, 371);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_Max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_Min)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.NumericUpDown numeric_Max;
        private System.Windows.Forms.NumericUpDown numeric_Min;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_Clear;
        private System.Windows.Forms.ComboBox comboBox_ChartType;
        private System.Windows.Forms.Label label1;
    }
}
