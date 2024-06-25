using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PlaceholderTextBoxControl
{
    public class PlaceholderTextBox : TextBox
    {
        [Category("Placeholder")]
        [Description("Set placeholder text")]
        public string PlaceholderText
        {
            get => _placeholderText;
            set
            {
                UpdateText(true);
                _placeholderText = value;
                UpdateText(false);
            }
        }

        [Category("Placeholder")]
        [Description("Set placeholder text color")]
        public Color PlaceholderTextColor
        {
            get => _placeholderColor;
            set
            {
                _placeholderColor = value;
                UpdateColor();
            }
        }

        [Category("Appearance")]
        [Description("A Color that represents the control's foreground color.")]
        public new Color ForeColor
        {
            get => _foreColor;
            set
            {
                _foreColor = value;
                UpdateColor();
            }
        }

        private string _placeholderText = "Input text";
        private Color _placeholderColor = Color.DarkGray;
        private Color _foreColor = SystemColors.WindowText;

        public PlaceholderTextBox()
        {
            GotFocus += (s, e) => { UpdateSetting(); };
            LostFocus += (s, e) => { UpdateSetting(); };
            TextChanged += (s, e) => { UpdateSetting(); };

            UpdateText();
            UpdateColor();
        }

        private void UpdateSetting()
        {
            UpdateText(Focused);
            UpdateColor();
        }

        private void UpdateText(bool focused = false)
        {
            if (focused && string.Equals(Text, PlaceholderText))
                Text = string.Empty;
            else if (!focused && string.IsNullOrWhiteSpace(Text))
                Text = PlaceholderText;
        }

        private void UpdateColor()
        {
            if (Focused)
                base.ForeColor = ForeColor;
            else
            {
                if (string.Equals(Text, PlaceholderText))
                    base.ForeColor = PlaceholderTextColor;
                else
                    base.ForeColor = ForeColor;
            }
        }
    }
}