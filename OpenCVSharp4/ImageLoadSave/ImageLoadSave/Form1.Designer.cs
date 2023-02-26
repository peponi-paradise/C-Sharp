namespace ImageLoadSave
{
    partial class MainFrame
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
            this.MainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.PictureView = new System.Windows.Forms.PictureBox();
            this.ControlPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ImagePath = new System.Windows.Forms.TextBox();
            this.LoadButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ImagePathBox = new System.Windows.Forms.GroupBox();
            this.ImageModeBox = new System.Windows.Forms.GroupBox();
            this.ImageMode = new System.Windows.Forms.ListBox();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureView)).BeginInit();
            this.ControlPanel.SuspendLayout();
            this.ImagePathBox.SuspendLayout();
            this.ImageModeBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.ColumnCount = 1;
            this.MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainPanel.Controls.Add(this.PictureView, 0, 0);
            this.MainPanel.Controls.Add(this.ControlPanel, 0, 1);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.RowCount = 2;
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.MainPanel.Size = new System.Drawing.Size(1008, 729);
            this.MainPanel.TabIndex = 0;
            // 
            // PictureView
            // 
            this.PictureView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureView.Location = new System.Drawing.Point(1, 1);
            this.PictureView.Margin = new System.Windows.Forms.Padding(1);
            this.PictureView.Name = "PictureView";
            this.PictureView.Size = new System.Drawing.Size(1006, 581);
            this.PictureView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureView.TabIndex = 0;
            this.PictureView.TabStop = false;
            // 
            // ControlPanel
            // 
            this.ControlPanel.ColumnCount = 3;
            this.ControlPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.ControlPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.ControlPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.ControlPanel.Controls.Add(this.SaveButton, 2, 1);
            this.ControlPanel.Controls.Add(this.LoadButton, 2, 0);
            this.ControlPanel.Controls.Add(this.ImagePathBox, 0, 0);
            this.ControlPanel.Controls.Add(this.ImageModeBox, 1, 0);
            this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ControlPanel.Location = new System.Drawing.Point(0, 583);
            this.ControlPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.RowCount = 2;
            this.ControlPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ControlPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ControlPanel.Size = new System.Drawing.Size(1008, 146);
            this.ControlPanel.TabIndex = 1;
            // 
            // ImagePath
            // 
            this.ImagePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImagePath.Location = new System.Drawing.Point(1, 26);
            this.ImagePath.Margin = new System.Windows.Forms.Padding(1);
            this.ImagePath.Multiline = true;
            this.ImagePath.Name = "ImagePath";
            this.ImagePath.Size = new System.Drawing.Size(331, 117);
            this.ImagePath.TabIndex = 0;
            // 
            // LoadButton
            // 
            this.LoadButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoadButton.Location = new System.Drawing.Point(671, 1);
            this.LoadButton.Margin = new System.Windows.Forms.Padding(1);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(336, 71);
            this.LoadButton.TabIndex = 2;
            this.LoadButton.Text = "LOAD";
            this.LoadButton.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            this.SaveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SaveButton.Location = new System.Drawing.Point(671, 74);
            this.SaveButton.Margin = new System.Windows.Forms.Padding(1);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(336, 71);
            this.SaveButton.TabIndex = 3;
            this.SaveButton.Text = "SAVE";
            this.SaveButton.UseVisualStyleBackColor = true;
            // 
            // ImagePathBox
            // 
            this.ImagePathBox.Controls.Add(this.ImagePath);
            this.ImagePathBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImagePathBox.Location = new System.Drawing.Point(1, 1);
            this.ImagePathBox.Margin = new System.Windows.Forms.Padding(1);
            this.ImagePathBox.Name = "ImagePathBox";
            this.ImagePathBox.Padding = new System.Windows.Forms.Padding(1);
            this.ControlPanel.SetRowSpan(this.ImagePathBox, 2);
            this.ImagePathBox.Size = new System.Drawing.Size(333, 144);
            this.ImagePathBox.TabIndex = 4;
            this.ImagePathBox.TabStop = false;
            this.ImagePathBox.Text = "Image Path";
            // 
            // ImageModeBox
            // 
            this.ImageModeBox.Controls.Add(this.ImageMode);
            this.ImageModeBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImageModeBox.Location = new System.Drawing.Point(336, 1);
            this.ImageModeBox.Margin = new System.Windows.Forms.Padding(1);
            this.ImageModeBox.Name = "ImageModeBox";
            this.ImageModeBox.Padding = new System.Windows.Forms.Padding(1);
            this.ControlPanel.SetRowSpan(this.ImageModeBox, 2);
            this.ImageModeBox.Size = new System.Drawing.Size(333, 144);
            this.ImageModeBox.TabIndex = 5;
            this.ImageModeBox.TabStop = false;
            this.ImageModeBox.Text = "Image Mode";
            // 
            // ImageMode
            // 
            this.ImageMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImageMode.FormattingEnabled = true;
            this.ImageMode.IntegralHeight = false;
            this.ImageMode.ItemHeight = 24;
            this.ImageMode.Location = new System.Drawing.Point(1, 26);
            this.ImageMode.Margin = new System.Windows.Forms.Padding(1);
            this.ImageMode.Name = "ImageMode";
            this.ImageMode.Size = new System.Drawing.Size(331, 117);
            this.ImageMode.TabIndex = 0;
            // 
            // MainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.MainPanel);
            this.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "MainFrame";
            this.Text = "Image Load/Save";
            this.MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureView)).EndInit();
            this.ControlPanel.ResumeLayout(false);
            this.ImagePathBox.ResumeLayout(false);
            this.ImagePathBox.PerformLayout();
            this.ImageModeBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainPanel;
        private System.Windows.Forms.PictureBox PictureView;
        private System.Windows.Forms.TableLayoutPanel ControlPanel;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.TextBox ImagePath;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.GroupBox ImagePathBox;
        private System.Windows.Forms.GroupBox ImageModeBox;
        private System.Windows.Forms.ListBox ImageMode;
    }
}

