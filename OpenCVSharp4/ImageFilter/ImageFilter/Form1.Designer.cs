namespace ImageFilter
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
            this.ExecuteFilter2D = new System.Windows.Forms.Button();
            this.ExecuteGaussianBlur = new System.Windows.Forms.Button();
            this.ExecuteBoxFilter = new System.Windows.Forms.Button();
            this.ExecuteMedianBlur = new System.Windows.Forms.Button();
            this.Input = new System.Windows.Forms.PictureBox();
            this.Result = new System.Windows.Forms.PictureBox();
            this.ExecuteBilateralFilter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Input)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Result)).BeginInit();
            this.SuspendLayout();
            // 
            // ExecuteFilter2D
            // 
            this.ExecuteFilter2D.Location = new System.Drawing.Point(12, 12);
            this.ExecuteFilter2D.Name = "ExecuteFilter2D";
            this.ExecuteFilter2D.Size = new System.Drawing.Size(185, 38);
            this.ExecuteFilter2D.TabIndex = 2;
            this.ExecuteFilter2D.Text = "Filter2D";
            this.ExecuteFilter2D.UseVisualStyleBackColor = true;
            // 
            // ExecuteGaussianBlur
            // 
            this.ExecuteGaussianBlur.Location = new System.Drawing.Point(394, 12);
            this.ExecuteGaussianBlur.Name = "ExecuteGaussianBlur";
            this.ExecuteGaussianBlur.Size = new System.Drawing.Size(185, 38);
            this.ExecuteGaussianBlur.TabIndex = 2;
            this.ExecuteGaussianBlur.Text = "Gaussian Blur";
            this.ExecuteGaussianBlur.UseVisualStyleBackColor = true;
            // 
            // ExecuteBoxFilter
            // 
            this.ExecuteBoxFilter.Location = new System.Drawing.Point(203, 12);
            this.ExecuteBoxFilter.Name = "ExecuteBoxFilter";
            this.ExecuteBoxFilter.Size = new System.Drawing.Size(185, 38);
            this.ExecuteBoxFilter.TabIndex = 2;
            this.ExecuteBoxFilter.Text = "Box Filter";
            this.ExecuteBoxFilter.UseVisualStyleBackColor = true;
            // 
            // ExecuteMedianBlur
            // 
            this.ExecuteMedianBlur.Location = new System.Drawing.Point(585, 12);
            this.ExecuteMedianBlur.Name = "ExecuteMedianBlur";
            this.ExecuteMedianBlur.Size = new System.Drawing.Size(185, 38);
            this.ExecuteMedianBlur.TabIndex = 2;
            this.ExecuteMedianBlur.Text = "Median Blur";
            this.ExecuteMedianBlur.UseVisualStyleBackColor = true;
            // 
            // Input
            // 
            this.Input.Location = new System.Drawing.Point(12, 100);
            this.Input.Name = "Input";
            this.Input.Size = new System.Drawing.Size(376, 338);
            this.Input.TabIndex = 3;
            this.Input.TabStop = false;
            // 
            // Result
            // 
            this.Result.Location = new System.Drawing.Point(394, 100);
            this.Result.Name = "Result";
            this.Result.Size = new System.Drawing.Size(376, 338);
            this.Result.TabIndex = 3;
            this.Result.TabStop = false;
            // 
            // ExecuteBilateralFilter
            // 
            this.ExecuteBilateralFilter.Location = new System.Drawing.Point(12, 56);
            this.ExecuteBilateralFilter.Name = "ExecuteBilateralFilter";
            this.ExecuteBilateralFilter.Size = new System.Drawing.Size(185, 38);
            this.ExecuteBilateralFilter.TabIndex = 2;
            this.ExecuteBilateralFilter.Text = "Bilateral Filter";
            this.ExecuteBilateralFilter.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Result);
            this.Controls.Add(this.Input);
            this.Controls.Add(this.ExecuteMedianBlur);
            this.Controls.Add(this.ExecuteBoxFilter);
            this.Controls.Add(this.ExecuteGaussianBlur);
            this.Controls.Add(this.ExecuteBilateralFilter);
            this.Controls.Add(this.ExecuteFilter2D);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Input)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Result)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button ExecuteFilter2D;
        private System.Windows.Forms.Button ExecuteGaussianBlur;
        private System.Windows.Forms.Button ExecuteBoxFilter;
        private System.Windows.Forms.Button ExecuteMedianBlur;
        private System.Windows.Forms.PictureBox Input;
        private System.Windows.Forms.PictureBox Result;
        private System.Windows.Forms.Button ExecuteBilateralFilter;
    }
}

