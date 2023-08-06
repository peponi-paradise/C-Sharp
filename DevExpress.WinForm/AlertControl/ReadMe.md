## Introduction

<br>

![screenshot](alertcontrol.png)

- 위의 그림과 같이, 윈도우 알림 화면을 DevExpress에서는 [Alert Windows](https://docs.devexpress.com/WindowsForms/5487/controls-and-libraries/messages-notifications-and-dialogs/alert-windows)로 제공하고 있다.
- 비슷한 기능으로 [Toast notification](https://learn.microsoft.com/en-us/previous-versions/windows/apps/hh779727(v=win.10)#sending-toast-notifications-from-desktop-apps) 또한 존재한다. 이는 윈도우 알림메세지 (Win10 기준, 작업표시줄 끝에 위치) 까지 연결된다.
    (참조 - [C# - DevExpress - ToastNotificationsManager](https://peponi-paradise.tistory.com/entry/C-DevExpress-ToastNotificationsManager))
- 여기서는 HTML/CSS 템플릿 적용 예제를 다뤄본다. [기본 컨트롤](https://docs.devexpress.com/WindowsForms/DevExpress.XtraBars.Alerter.AlertControl)도 깔끔하게 구현되어 있지만, 여러가지 데이터를 넣기에는 용이하지 않아 직접 제작하였다.
    <strike>_필자는 HTML 및 CSS를 잘 다루지 못해 굉장히 지저분하게 만들어졌다.._</strike>

<br>

## Code

<br>

### HTML

<br>

```html
<div class="container">
    <div class="popup">
        <div class="title-container">
            <div class="title-caption">
                <img class="titleImageSource" src='${TitleImageSource}' />
                <div class="titleCaption">${Title}</div>
            </div>
            <div class="title-buttons">
                <img id="pinButton" class="pin-button" src='${TitlePinImageSource}' />
                <img id="closeButton" class="close-button" src='${TitleCloseImageSource}' />
            </div>
        </div>
        <div class="body-container">
            <div class="body-caption-container">
                <div class="bodyCaption">${Caption}</div>
            </div>
            <div class="body-description-container">
                <div class="body-description-image">
                    <img class="bodyDescriptionImageSource" src='${DescriptionImageSource}' />
                </div>
                <div class="body-description-text">
                    <div class="bodyDescription1">${Description1}</div>
                    <div class="bodyDescription2">${Description2}</div>
                </div>
            </div>
        </div>
        <div class="footer-container">
            <div class="footer-url">
                <a href='${FooterUrl1}'>${FooterUrl1}</a><br>
                <a href='${FooterUrl2}'>${FooterUrl2}</a>
            </div>
            <div class="footer-copyright">
                <br><div class="copyright">${Copyright}</div>
            </div>
        </div>
    </div>
</div>
```

<br>

### CSS

<br>

```css
.container {
    padding: 6px 14px 20px 14px;
}

.popup {
    width: 350px;
    height: auto;
    padding: 4px;
    border-radius: 10px;
    box-shadow: 0px 8px 16px rgba(0,0,0,0.25);
    border: 1px solid @WindowText/0.2;
    display: flex;
    flex-direction: column;
    font-family: "Segoe UI";
    justify-content: space-around;
    background-color: @Control;
    color: @ControlText;
}

.title-container {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    font-family: 'Segoe UI Semibold';
    font-size: large;
}

.title-caption {
    padding: 10px 10px 0px 10px;
    display: flex;
    flex-direction: row;
}

.titleImageSource {
    width: 18px;
    height: 18px;
}

.titleCaption {
    padding-left: 10px;
}

.title-buttons {
    display: flex;
    flex-direction: row;
    padding: 5px 2px 0px 0px;
}

.pin-button {
    width: 18px;
    height: 18px;
    padding: 5px;
    border-radius: 2px;
    align-self: center;
}

    .pin-button:hover {
        border: solid 1px;
    }

.close-button {
    width: 18px;
    height: 18px;
    padding: 5px;
    border-radius: 2px;
    align-self: center;
}

    .close-button:hover {
        border: solid 1px;
    }

.body-container {
    padding: 10px;
}

.bodyCaption {
    padding-left: 60px;
    padding-bottom: 10px;
    font-family: 'Segoe UI Semibold';
    font-size: large;
}

.body-description-container {
    display: flex;
    flex-direction: row;
    padding-bottom: 10px;
}

.bodyDescriptionImageSource {
    width: 50px;
    height: 50px;
}

.body-description-text {
    padding-left: 10px;
    padding-bottom: 10px;
    font-family: 'Segoe UI';
    font-size: medium;
}

.footer-container {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    padding: 10px;
    font-family: 'Segoe UI';
    font-size: small;
}
```

<br>

### Alert Data

<br>

```cs
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
```

<br>

### AlertWindow Image Init

<br>

```cs
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
```

<br>

### AlertWindow Init

<br>

```cs
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
```

<br>

### Form1.cs

<br>

```cs
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
}
```