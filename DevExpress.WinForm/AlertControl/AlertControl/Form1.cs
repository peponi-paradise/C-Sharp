using DevExpress.Utils;
using System.IO;

namespace AlertControl
{
    public partial class Form1 : Form
    {
        private AlertData AlertData;
        private DevExpress.XtraBars.Alerter.AlertControl AlertWindow;
        private SvgImageCollection ImageCollection;

        public Form1()
        {
            InitializeComponent();
            InitImageCollection();
            InitAlert();
        }

        private void button1_Click(object sender, EventArgs e) => AlertWindow.Show(this);

        private void InitImageCollection()
        {
            // Image collection 초기화

            var asset = $@"{Application.StartupPath}\Assets\";
            ImageCollection = new SvgImageCollection(components)
            {
                { "ShortDate", $@"{asset}ShortDate.svg" },
                {"UnpinButton",$@"{asset}UnpinButton.svg" },
                {"PinButton",$@"{asset}PinButton.svg" },
                {"Delete",$@"{asset}Delete.svg" }
            };
        }

        private void InitAlert()
        {
            // AlertData

            AlertData = new AlertData()
            {
                Title = "Alert Control",
                TitleImageSource = ImageCollection["ShortDate"],
                TitlePinImageSource = ImageCollection["UnpinButton"],
                TitleCloseImageSource = ImageCollection["Delete"],
                Caption = "Caption",
                DescriptionImageSource = ImageCollection["ShortDate"],
                Description1 = "Description1",
                Description2 = "Description2",
                FooterUrl1 = "https://github.com/peponi-paradise/",
                FooterUrl2 = "https://peponi-paradise.tistory.com/",
                Copyright = $"©Peponi, {DateTime.Now.Year}",
            };

            // AlertWindow

            AlertWindow = new DevExpress.XtraBars.Alerter.AlertControl(components);
            AlertWindow.AppearanceText.Options.UseTextOptions = true;
            AlertWindow.AppearanceText.TextOptions.Trimming = DevExpress.Utils.Trimming.Word;
            AlertWindow.AppearanceText.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            AlertWindow.ShowToolTips = false;

            AlertWindow.BeforeFormShow += AlertWindow_BeforeFormShow;
            AlertWindow.HtmlElementMouseClick += AlertWindow_HtmlElementMouseClick;
            AlertWindow.FormClosing += AlertWindow_FormClosing;

            // HTML / CSS
            AlertWindow.HtmlTemplate.Template += File.ReadAllText($@"{Application.StartupPath}\Assets\AlertWindow.html");
            AlertWindow.HtmlTemplate.Styles += File.ReadAllText($@"{Application.StartupPath}\Assets\AlertWindow.css");
        }

        private void AlertWindow_BeforeFormShow(object sender, DevExpress.XtraBars.Alerter.AlertFormEventArgs e)
        {
            e.HtmlPopup.DataContext = AlertData;
        }

        private void AlertWindow_HtmlElementMouseClick(object sender, DevExpress.XtraBars.Alerter.AlertHtmlElementMouseEventArgs e)
        {
            if (e.ElementId == "pinButton")
            {
                e.HtmlPopup.Pinned = !e.HtmlPopup.Pinned;
                if (e.HtmlPopup.Pinned) AlertData.TitlePinImageSource = ImageCollection["PinButton"];
                else AlertData.TitlePinImageSource = ImageCollection["UnpinButton"];
            }
            else if (e.ElementId == "closeButton") e.HtmlPopup.Close();
        }

        private void AlertWindow_FormClosing(object sender, DevExpress.XtraBars.Alerter.AlertFormClosingEventArgs e)
        {
            AlertData.TitlePinImageSource = ImageCollection["UnpinButton"];
        }
    }
}