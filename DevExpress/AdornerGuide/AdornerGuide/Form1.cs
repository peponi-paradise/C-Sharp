using DevExpress.Utils.VisualEffects;
using System;
using System.Collections.Generic;

namespace AdornerGuide
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        private AdornerUIManager Manager;

        public Form1()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Manager = new AdornerUIManager();
            Manager.Owner = this;

            var guideItems = GetGuideInformation();
            AdornerGuide guide = new AdornerGuide(guideItems, Manager);
            guide.StartGuide();
        }

        private List<GuideInformation> GetGuideInformation()
        {
            List<GuideInformation> guideInformation = new List<GuideInformation>
            {
                new GuideInformation()
                {
                    TargetControl = simpleButton1,
                    Description = "This is Fitst guide"
                },
                new GuideInformation()
                {
                    TargetControl = simpleButton2,
                    Description = "This is Second guide"
                },
                new GuideInformation()
                {
                    TargetControl = simpleButton3,
                    Description = "This is Third guide"
                }
            };
            return guideInformation;
        }
    }
}