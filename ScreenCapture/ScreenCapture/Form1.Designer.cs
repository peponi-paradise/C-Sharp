namespace ScreenCapture
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
            this.Container = new System.Windows.Forms.TableLayoutPanel();
            this.Control = new System.Windows.Forms.Button();
            this.Container.SuspendLayout();
            this.SuspendLayout();
            // 
            // Container
            // 
            this.Container.ColumnCount = 2;
            this.Container.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Container.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Container.Controls.Add(this.Control, 1, 0);
            this.Container.Location = new System.Drawing.Point(38, 41);
            this.Container.Name = "Container";
            this.Container.RowCount = 2;
            this.Container.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Container.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Container.Size = new System.Drawing.Size(718, 397);
            this.Container.TabIndex = 0;
            // 
            // Control
            // 
            this.Control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Control.Location = new System.Drawing.Point(362, 3);
            this.Control.Name = "Control";
            this.Control.Size = new System.Drawing.Size(353, 192);
            this.Control.TabIndex = 0;
            this.Control.Text = "This area will be captured";
            this.Control.UseVisualStyleBackColor = true;
            this.Control.Click += new System.EventHandler(this.Control_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Container);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Container.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel Container;
        private System.Windows.Forms.Button Control;
    }
}

