using DevExpress.Utils.VisualEffects;
using System.Collections.Generic;

namespace AdornerGuide
{
    public record GuideInformation
    {
        public string Description;
        public object TargetControl;
    }

    public class AdornerGuide
    {
        /*-------------------------------------------
         *
         *      Public members
         *
         -------------------------------------------*/

        public List<GuideInformation> GuideList;

        /*-------------------------------------------
         *
         *      Private members
         *
         -------------------------------------------*/

        private AdornerGuideFlyout Flyout;
        private AdornerUIManager Manager;
        private Guide Guide;

        /*-------------------------------------------
         *
         *      Constructor / Destructor
         *
         -------------------------------------------*/

        public AdornerGuide(List<GuideInformation> guideList, AdornerUIManager manager)
        {
            GuideList = guideList;
            Manager = manager;
            Guide = new Guide();
            Flyout = new AdornerGuideFlyout(this, guideList.Count);
            Manager.Elements.Add(Guide);
            Manager.QueryGuideFlyoutControl += Manager_QueryGuideFlyoutControl;
        }

        ~AdornerGuide()
        {
        }

        /*-------------------------------------------
         *
         *      Event functions
         *
         -------------------------------------------*/

        private void Manager_QueryGuideFlyoutControl(object sender, QueryGuideFlyoutControlEventArgs e)
        {
            e.Control = Flyout;
        }

        /*-------------------------------------------
         *
         *      Public functions
         *
         -------------------------------------------*/

        public void StartGuide()
        {
            Manager.ShowGuides = DevExpress.Utils.DefaultBoolean.True;
            SetGuide(Flyout.CurrentGuideIndex);
        }

        public void EndGuide()
        {
            Manager.ShowGuides = DevExpress.Utils.DefaultBoolean.Default;
        }

        public void SetGuide(int index)
        {
            if (index < 0 || index > GuideList.Count - 1) return;
            Flyout.LabelText = GuideList[index].Description;
            Guide.TargetElement = GuideList[index].TargetControl;
        }
    }
}