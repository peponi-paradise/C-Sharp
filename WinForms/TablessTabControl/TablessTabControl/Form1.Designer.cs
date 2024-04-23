namespace TablessTabControl
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
            tablessTabControl1 = new CustomTabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tablessTabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // tablessTabControl1
            // 
            tablessTabControl1.Controls.Add(tabPage1);
            tablessTabControl1.Controls.Add(tabPage2);
            tablessTabControl1.Location = new Point(528, 253);
            tablessTabControl1.Name = "tablessTabControl1";
            tablessTabControl1.SelectedIndex = 0;
            tablessTabControl1.ShowTab = true;
            tablessTabControl1.Size = new Size(204, 165);
            tablessTabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(196, 137);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(196, 137);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tablessTabControl1);
            Name = "Form1";
            Text = "Form1";
            tablessTabControl1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private CustomTabControl tablessTabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
    }
}
