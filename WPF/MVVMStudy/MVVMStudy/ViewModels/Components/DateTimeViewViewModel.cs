using DevExpress.Mvvm;
using DevExpress.Mvvm.CodeGenerators;
using MVVMStudy.Models;
using System;

namespace MVVMStudy.ViewModels.Components
{
    [GenerateViewModel]
    public partial class DateTimeViewViewModel
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

        public DateTimeViewViewModel()
        {
            DateTime = new DateTimeModel();
            Messenger.Default.Register<DateTime>(this, ReceiveDateTime);
        }

        ~DateTimeViewViewModel()
        {
        }

        /*-------------------------------------------
         *
         *      Event functions
         *
         -------------------------------------------*/

        private void ReceiveDateTime(DateTime dateTime)
        {
            DateTime.DateTime = dateTime;
        }

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