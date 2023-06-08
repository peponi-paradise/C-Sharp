using DevExpress.Mvvm.CodeGenerators;
using MVVMStudy.ViewModels.Components;

namespace MVVMStudy.ViewModels.Windows
{
    [GenerateViewModel]
    public partial class MainViewModel
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
        public DateTimeSenderViewModel _SenderViewModel;

        [GenerateProperty]
        public DateTimeViewViewModel _ViewViewModel;

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

        public MainViewModel(DateTimeSenderViewModel senderViewModel, DateTimeViewViewModel viewViewModel)
        {
            _SenderViewModel = senderViewModel;
            _ViewViewModel = viewViewModel;
        }

        ~MainViewModel()
        {
        }

        /*-------------------------------------------
         *
         *      Event functions
         *
         -------------------------------------------*/

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