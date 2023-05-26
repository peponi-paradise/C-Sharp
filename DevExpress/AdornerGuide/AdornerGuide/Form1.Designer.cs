namespace AdornerGuide
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            StartButton = new DevExpress.XtraEditors.SimpleButton();
            SuspendLayout();
            // 
            // simpleButton1
            // 
            simpleButton1.Location = new System.Drawing.Point(89, 84);
            simpleButton1.Name = "simpleButton1";
            simpleButton1.Size = new System.Drawing.Size(111, 104);
            simpleButton1.TabIndex = 0;
            simpleButton1.Text = "simpleButton1";
            // 
            // simpleButton2
            // 
            simpleButton2.Location = new System.Drawing.Point(206, 84);
            simpleButton2.Name = "simpleButton2";
            simpleButton2.Size = new System.Drawing.Size(111, 104);
            simpleButton2.TabIndex = 0;
            simpleButton2.Text = "simpleButton1";
            // 
            // simpleButton3
            // 
            simpleButton3.Location = new System.Drawing.Point(323, 84);
            simpleButton3.Name = "simpleButton3";
            simpleButton3.Size = new System.Drawing.Size(111, 104);
            simpleButton3.TabIndex = 0;
            simpleButton3.Text = "simpleButton1";
            // 
            // StartButton
            // 
            StartButton.Location = new System.Drawing.Point(614, 84);
            StartButton.Name = "StartButton";
            StartButton.Size = new System.Drawing.Size(111, 104);
            StartButton.TabIndex = 0;
            StartButton.Text = "start";
            StartButton.Click += StartButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(737, 299);
            Controls.Add(StartButton);
            Controls.Add(simpleButton3);
            Controls.Add(simpleButton2);
            Controls.Add(simpleButton1);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton StartButton;
    }
}

