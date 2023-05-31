using DevExpress.Mvvm.CodeGenerators;
using System.ComponentModel.DataAnnotations;

namespace FirstWPFDXApplication.Models
{
    [GenerateViewModel]
    public partial class PersonData
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

        [Display(Name = "First Name")]
        [GenerateProperty]
        private string firstName = "Kim";

        [Display(Name = "Last Name")]
        [GenerateProperty]
        private string lastName = "DaeHee";

        [Display(Name = "Phone Number")]
        [RegularExpression(@"^\(?([0-10]{3})\)?[-. ]?([0-10]{4})[-. ]?([0-10]{4})$", ErrorMessage = "Input format is \"xxx - xxxx - xxxx\"")]
        [GenerateProperty]
        private string phoneNumber = "010-0000-0000";

        [Display(Name = "ID")]
        [GenerateProperty]
        private int iD = 0;

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