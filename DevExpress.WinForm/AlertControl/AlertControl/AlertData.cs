using DevExpress.Utils.Svg;

namespace AlertControl
{
    public class AlertData
    {
        public string Title { get; set; } = "";
        public SvgImage TitleImageSource { get; set; }
        public SvgImage TitlePinImageSource { get; set; }
        public SvgImage TitleCloseImageSource { get; set; }
        public string Caption { get; set; } = "";
        public SvgImage DescriptionImageSource { get; set; }
        public string Description1 { get; set; } = "";
        public string Description2 { get; set; } = "";
        public string FooterUrl1 { get; set; } = "";
        public string FooterUrl2 { get; set; } = "";
        public string Copyright { get; set; } = "";
    }
}