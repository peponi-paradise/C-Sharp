using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToastNotification
{
    public partial class Form1 : Form
    {
        private string Header = "Hello Header";
        private string Body = "This is Body 1";
        private string Body2 = "This is Body 2";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            toastNotificationsManager.Notifications[0].AttributionText = "Peponi";
            toastNotificationsManager.Notifications[0].Header = Header;
            toastNotificationsManager.Notifications[0].Body = Body;
            toastNotificationsManager.Notifications[0].Body2 = Body2;
            toastNotificationsManager.ShowNotification(toastNotificationsManager.Notifications[0]);
        }
    }
}