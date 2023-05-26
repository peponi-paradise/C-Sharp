using System;

namespace AdornerGuide
{
    public partial class AdornerGuideFlyout : DevExpress.XtraEditors.XtraUserControl
    {
        /*-------------------------------------------
         *
         *      Public members
         *
         -------------------------------------------*/

        public string LabelText
        {
            get { return label.Text; }
            set { label.Text = value; }
        }

        public int CurrentGuideIndex = 0;

        /*-------------------------------------------
         *
         *      Private members
         *
         -------------------------------------------*/

        private AdornerGuide Guide;

        /*-------------------------------------------
         *
         *      Constructor / Destructor
         *
         -------------------------------------------*/

        public AdornerGuideFlyout()
        {
            InitializeComponent();
        }

        public AdornerGuideFlyout(AdornerGuide guide, int guideCount)
        {
            InitializeComponent();
            Guide = guide;
            InitializeNavigator(guideCount);
        }

        /*-------------------------------------------
         *
         *      Event functions
         *
         -------------------------------------------*/

        private void OnNavigatorButtonChecked(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            CurrentGuideIndex = navigator.Buttons.IndexOf(e.Button);
            Guide.SetGuide(CurrentGuideIndex);
        }

        private void OnExitButtonClick(object sender, EventArgs e)
        {
            Guide.EndGuide();
        }

        private void OnBackButtonClick(object sender, EventArgs e)
        {
            CurrentGuideIndex--;
            if (CurrentGuideIndex < 0)
                CurrentGuideIndex = navigator.Buttons.Count - 1;
            navigator.Buttons[CurrentGuideIndex].Properties.Checked = true;
        }

        private void OnNextButtonClick(object sender, EventArgs e)
        {
            CurrentGuideIndex++;
            if (CurrentGuideIndex > navigator.Buttons.Count - 1)
                CurrentGuideIndex = 0;
            navigator.Buttons[CurrentGuideIndex].Properties.Checked = true;
        }

        /*-------------------------------------------
         *
         *      Private functions
         *
         -------------------------------------------*/

        private void InitializeNavigator(int count)
        {
            for (int i = 0; i < count; i++)
            {
                this.navigator.Buttons.Add(
                new DevExpress.XtraBars.Docking2010.WindowsUIButton("button", "appointmentnightclock", DevExpress.XtraBars.Docking2010.ImageLocation.Default, DevExpress.XtraBars.Docking2010.ButtonStyle.CheckButton, "", false, i, true, null, true, i == CurrentGuideIndex, true, null, null, 1, false, false));
            }
        }
    }
}