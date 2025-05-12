using DevExpress.Utils;
using System.IO;

namespace AlertControl
{
    public partial class Form1 : Form
    {
        private readonly DevExpress.XtraBars.Alerter.AlertControl _alert;
        private readonly SvgImageCollection _images;

        public Form1()
        {
            InitializeComponent();
            _images = InitImageCollection();
            _alert = InitAlert();

            _alert.Show(this);
        }

        private void button1_Click(object sender, EventArgs e) => _alert.Show(this);

        private SvgImageCollection InitImageCollection()
        {
            var basePath = $@"{Application.StartupPath}\Assets\";
            return new SvgImageCollection(components)
            {
                { "ShortDate", $@"{basePath}ShortDate.svg" },
                { "UnpinButton", $@"{basePath}UnpinButton.svg" },
                { "PinButton", $@"{basePath}PinButton.svg" },
                { "Delete", $@"{basePath}Delete.svg" }
            };
        }

        private AlertData GetAlertData()
        {
            return new AlertData()
            {
                Title = "Alert Control",
                TitleImageSource = _images["ShortDate"],
                TitlePinImageSource = _images["UnpinButton"],
                TitleCloseImageSource = _images["Delete"],
                Caption = "Caption",
                DescriptionImageSource = _images["ShortDate"],
                Description1 = "Description1",
                Description2 = "Description2",
                FooterUrl1 = "https://github.com/peponi-paradise/",
                FooterUrl2 = "https://peponi-paradise.vercel.app/",
                Copyright = $"©Peponi, {DateTime.Now.Year}",
            };
        }

        private DevExpress.XtraBars.Alerter.AlertControl InitAlert()
        {
            var alert = new DevExpress.XtraBars.Alerter.AlertControl(components);

            alert.BeforeFormShow += Alert_BeforeFormShow;
            alert.HtmlElementMouseClick += Alert_HtmlElementMouseClick;

            // HTML / CSS
            alert.HtmlTemplate.Template += File.ReadAllText($@"{Application.StartupPath}\Assets\AlertWindow.html");
            alert.HtmlTemplate.Styles += File.ReadAllText($@"{Application.StartupPath}\Assets\AlertWindow.css");

            return alert;
        }

        private void Alert_BeforeFormShow(object sender, DevExpress.XtraBars.Alerter.AlertFormEventArgs e)
        {
            e.HtmlPopup.DataContext = GetAlertData();
        }

        private void Alert_HtmlElementMouseClick(object sender, DevExpress.XtraBars.Alerter.AlertHtmlElementMouseEventArgs e)
        {
            if (e.ElementId == "pinButton")
            {
                e.HtmlPopup.Pinned = !e.HtmlPopup.Pinned;

                var data = e.HtmlPopup.DataContext as AlertData;

                if (e.HtmlPopup.Pinned)
                    data!.TitlePinImageSource = _images["PinButton"];
                else
                    data!.TitlePinImageSource = _images["UnpinButton"];
            }
            else if (e.ElementId == "closeButton")
                e.HtmlPopup.Close();
        }
    }
}