using DevExpress.Data;
using DevExpress.XtraEditors;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;

namespace ToastNotification
{
    public partial class Form1 : Form
    {
        private readonly DevExpress.XtraBars.ToastNotifications.ToastNotificationsManager _notificationsManager;

        public Form1()
        {
            InitializeComponent();
            _notificationsManager = InitToastNotification();

            // Win10 스타일의 알림 기능을 이용하려면 시작 메뉴에 바로가기 등록이 되어 있어야 한다.
            if (CheckAndCreateShortcut())
            {
                // 알림 내용 설정
                _notificationsManager.Notifications[0].AttributionText = "Peponi";
                _notificationsManager.Notifications[0].Header = "Hello world";
                _notificationsManager.Notifications[0].Body = "This is Body 1";
                _notificationsManager.Notifications[0].Body2 = "This is Body 2";

                // 알림 호출
                _notificationsManager.ShowNotification(_notificationsManager.Notifications[0]);
            }
            else
            {
                Process.GetCurrentProcess().Kill();
            }
        }

        private bool CheckAndCreateShortcut()
        {
            if (ShellHelper.IsApplicationShortcutExist(Application.ProductName))
                return true;
            else
            {
                ShellHelper.TryCreateShortcut(
                            applicationId: _notificationsManager.ApplicationId,
                            name: Application.ProductName);

                // 바로가기 추가 후 다시 시작해야 정상 적용됨
                XtraMessageBox.Show("Please restart application", "Work done");

                return false;
            }
        }

        private DevExpress.XtraBars.ToastNotifications.ToastNotificationsManager InitToastNotification()
        {
            var manager = new DevExpress.XtraBars.ToastNotifications.ToastNotificationsManager(components)
            {
                ApplicationId = "12b48acf-0e0a-49ef-8041-dceb872ed14d",
                ApplicationName = "ToastNotificationTest"
            };

            // 알림 추가
            manager.Notifications.AddRange(new DevExpress.XtraBars.ToastNotifications.IToastNotificationProperties[] {
            new DevExpress.XtraBars.ToastNotifications.ToastNotification("77b2bb7e-94e3-40b5-ac7a-eec2af0da5da",
            null,
            null,
            (System.Drawing.Image)ToastNotification.Properties.Resources.ResourceManager.GetObject("NotificationsManager.Notifications"),
            null,
            null,
            null,
            "Header",
            "Body",
            "Body2",
            "Peponi",
            DevExpress.XtraBars.ToastNotifications.ToastNotificationSound.NoSound,
            DevExpress.XtraBars.ToastNotifications.ToastNotificationDuration.Long,
            null,
            DevExpress.XtraBars.ToastNotifications.AppLogoCrop.Default,
            DevExpress.XtraBars.ToastNotifications.ToastNotificationTemplate.Generic)
            });

            manager.UpdateToastContent += NotificationsManager_UpdateToastContent;

            return manager;
        }

        private void NotificationsManager_UpdateToastContent(object sender, DevExpress.XtraBars.ToastNotifications.UpdateToastContentEventArgs e)
        {
            // Footer 추가하는 방법
            XmlDocument doc = e.ToastContent;
            XmlNode bindingNode = doc.GetElementsByTagName("binding").Item(0);
            if (bindingNode != null)
            {
                XmlElement group = doc.CreateElement("group");
                bindingNode.AppendChild(group);

                XmlElement subGroup = doc.CreateElement("subgroup");
                group.AppendChild(subGroup);

                XmlElement text = doc.CreateElement("text");
                subGroup.AppendChild(text);
                text.SetAttribute("hint-style", "base");
                text.InnerText = "https://github.com/peponi-paradise";

                text = doc.CreateElement("text");
                subGroup.AppendChild(text);
                text.SetAttribute("hint-style", "captionSubtle");
                text.InnerText = "https://peponi-paradise.vercel.app/";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 알림 내용 설정
            _notificationsManager.Notifications[0].AttributionText = "Peponi";
            _notificationsManager.Notifications[0].Header = "Hello world";
            _notificationsManager.Notifications[0].Body = "This is Big text";
            _notificationsManager.Notifications[0].Body2 = "This is Small text";

            // 알림 호출
            _notificationsManager.ShowNotification(_notificationsManager.Notifications[0]);
        }
    }
}