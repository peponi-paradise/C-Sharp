namespace TestApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            _startButton = new Button();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // _startButton
            // 
            _startButton.Location = new Point(57, 63);
            _startButton.Name = "_startButton";
            _startButton.Size = new Size(240, 202);
            _startButton.TabIndex = 0;
            _startButton.Text = "Start";
            _startButton.UseVisualStyleBackColor = true;
            _startButton.Click += _startButton_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(341, 28);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(443, 405);
            textBox1.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textBox1);
            Controls.Add(_startButton);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button _startButton;
        private TextBox textBox1;
    }
}
