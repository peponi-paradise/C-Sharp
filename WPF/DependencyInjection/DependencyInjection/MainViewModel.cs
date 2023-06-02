using DevExpress.Mvvm;
using DevExpress.Mvvm.CodeGenerators;

namespace DependencyInjection
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
        public string _DateTime = "";

        /*-------------------------------------------
         *
         *      Private members
         *
         -------------------------------------------*/

        private IDateTime _DateTimeService;
        private System.Timers.Timer _DateTimeServiceTimer;

        /*-------------------------------------------
         *
         *      Constructor / Destructor
         *
         -------------------------------------------*/

        public MainViewModel(IDateTime dateTime)
        {
            _DateTimeService = dateTime;
            _DateTimeServiceTimer = new System.Timers.Timer();
            _DateTimeServiceTimer.Interval = 500;
            _DateTimeServiceTimer.Elapsed += _DateTimeServiceTimer_Elapsed;
            _DateTimeServiceTimer.Start();
        }

        private void _DateTimeServiceTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            DateTime = _DateTimeService.GetDateTimeString();
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