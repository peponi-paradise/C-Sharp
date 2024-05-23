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
            _stopButton = new Button();
            SuspendLayout();
            // 
            // _startButton
            // 
            _startButton.Location = new Point(91, 129);
            _startButton.Name = "_startButton";
            _startButton.Size = new Size(240, 202);
            _startButton.TabIndex = 0;
            _startButton.Text = "Start";
            _startButton.UseVisualStyleBackColor = true;
            _startButton.Click += _startButton_Click;
            // 
            // _stopButton
            // 
            _stopButton.Location = new Point(419, 129);
            _stopButton.Name = "_stopButton";
            _stopButton.Size = new Size(240, 202);
            _stopButton.TabIndex = 1;
            _stopButton.Text = "Stop";
            _stopButton.UseVisualStyleBackColor = true;
            _stopButton.Click += _stopButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(_stopButton);
            Controls.Add(_startButton);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button _startButton;
        private Button _stopButton;
    }
}
