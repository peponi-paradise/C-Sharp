using DevExpress.Mvvm;
using DevExpress.Mvvm.CodeGenerators;
using MVVMStudy.Models;

namespace MVVMStudy.ViewModels.Components
{
    [GenerateViewModel]
    public partial class DateTimeSenderViewModel
    {
        /*-------------------------------------------
         *
         *      Events
         *
         -------------------------------------------*/

        /*-------------------------------------------
         *
         *      Public members
         *
         -------------------------------------------*/

        [GenerateProperty]
        public DateTimeModel _DateTime;

        /*-------------------------------------------
         *
         *      Private members
         *
         -------------------------------------------*/

        /*-------------------------------------------
         *
         *      Constructor / Destructor
         *
         -------------------------------------------*/

        public DateTimeSenderViewModel()
        {
            DateTime = new DateTimeModel();
        }

        ~DateTimeSenderViewModel()
        {
        }

        /*-------------------------------------------
         *
         *      Event functions
         *
         -------------------------------------------*/

        [GenerateCommand]
        public void SendDateTime() => Messenger.Default.Send(DateTime.DateTime);

        /*-------------------------------------------
         *
         *      Public functions
         *
         -------------------------------------------*/

        /*-------------------------------------------
         *
         *      Private functions
         *
         -------------------------------------------*/

        /*-------------------------------------------
         *
         *      Helper functions
         *
         -------------------------------------------*/
    }
}