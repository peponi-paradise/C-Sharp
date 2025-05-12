namespace INotifyPropertyChangedExample
{
    partial class TestControl
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
            this.MainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.TestBool = new System.Windows.Forms.CheckBox();
            this.TestInt = new System.Windows.Forms.NumericUpDown();
            this.TestDouble = new System.Windows.Forms.NumericUpDown();
            this.TestString = new System.Windows.Forms.TextBox();
            this.TestEnum = new System.Windows.Forms.TextBox();
            this.PropertyChange = new System.Windows.Forms.Button();
            this.EnumSelect = new System.Windows.Forms.ListBox();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TestInt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TestDouble)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.ColumnCount = 2;
            this.MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.MainPanel.Controls.Add(this.TestBool, 0, 0);
            this.MainPanel.Controls.Add(this.TestInt, 0, 1);
            this.MainPanel.Controls.Add(this.TestDouble, 0, 2);
            this.MainPanel.Controls.Add(this.TestString, 0, 3);
            this.MainPanel.Controls.Add(this.TestEnum, 0, 4);
            this.MainPanel.Controls.Add(this.PropertyChange, 1, 0);
            this.MainPanel.Controls.Add(this.EnumSelect, 1, 4);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Margin = new System.Windows.Forms.Padding(0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.RowCount = 5;
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.MainPanel.Size = new System.Drawing.Size(200, 300);
            this.MainPanel.TabIndex = 0;
            // 
            // TestBool
            // 
            this.TestBool.AutoSize = true;
            this.TestBool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TestBool.Location = new System.Drawing.Point(3, 3);
            this.TestBool.Name = "TestBool";
            this.TestBool.Size = new System.Drawing.Size(94, 54);
            this.TestBool.TabIndex = 0;
            this.TestBool.Text = "bool";
            this.TestBool.UseVisualStyleBackColor = true;
            // 
            // TestInt
            // 
            this.TestInt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TestInt.Location = new System.Drawing.Point(3, 63);
            this.TestInt.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.TestInt.Minimum = new decimal(new int[] {
            -727379969,
            232,
            0,
            -2147483648});
            this.TestInt.Name = "TestInt";
            this.TestInt.Size = new System.Drawing.Size(94, 21);
            this.TestInt.TabIndex = 1;
            // 
            // TestDouble
            // 
            this.TestDouble.DecimalPlaces = 5;
            this.TestDouble.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TestDouble.Location = new System.Drawing.Point(0, 120);
            this.TestDouble.Margin = new System.Windows.Forms.Padding(0);
            this.TestDouble.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.TestDouble.Minimum = new decimal(new int[] {
            -727379969,
            232,
            0,
            -2147483648});
            this.TestDouble.Name = "TestDouble";
            this.TestDouble.Size = new System.Drawing.Size(100, 21);
            this.TestDouble.TabIndex = 2;
            // 
            // TestString
            // 
            this.TestString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TestString.Location = new System.Drawing.Point(0, 180);
            this.TestString.Margin = new System.Windows.Forms.Padding(0);
            this.TestString.Name = "TestString";
            this.TestString.Size = new System.Drawing.Size(100, 21);
            this.TestString.TabIndex = 3;
            // 
            // TestEnum
            // 
            this.TestEnum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TestEnum.Location = new System.Drawing.Point(0, 240);
            this.TestEnum.Margin = new System.Windows.Forms.Padding(0);
            this.TestEnum.Name = "TestEnum";
            this.TestEnum.Size = new System.Drawing.Size(100, 21);
            this.TestEnum.TabIndex = 4;
            // 
            // PropertyChange
            // 
            this.PropertyChange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PropertyChange.Location = new System.Drawing.Point(103, 3);
            this.PropertyChange.Name = "PropertyChange";
            this.PropertyChange.Size = new System.Drawing.Size(94, 54);
            this.PropertyChange.TabIndex = 5;
            this.PropertyChange.Text = "Change";
            this.PropertyChange.UseVisualStyleBackColor = true;
            this.PropertyChange.Click += new System.EventHandler(this.PropertyChange_Click);
            // 
            // EnumSelect
            // 
            this.EnumSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EnumSelect.FormattingEnabled = true;
            this.EnumSelect.ItemHeight = 12;
            this.EnumSelect.Location = new System.Drawing.Point(103, 243);
            this.EnumSelect.Name = "EnumSelect";
            this.EnumSelect.Size = new System.Drawing.Size(94, 54);
            this.EnumSelect.TabIndex = 6;
            // 
            // TestControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.MainPanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TestControl";
            this.Size = new System.Drawing.Size(200, 300);
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TestInt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TestDouble)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainPanel;
        private System.Windows.Forms.CheckBox TestBool;
        private System.Windows.Forms.NumericUpDown TestInt;
        private System.Windows.Forms.NumericUpDown TestDouble;
        private System.Windows.Forms.TextBox TestString;
        private System.Windows.Forms.TextBox TestEnum;
        private System.Windows.Forms.Button PropertyChange;
        private System.Windows.Forms.ListBox EnumSelect;
    }
}
