using DevExpress.Mvvm.CodeGenerators;

namespace PersonalPlanner.ViewModels.Pages
{
    [GenerateViewModel]
    public partial class MainViewModel
    {
        [GenerateProperty]
        private string _Status;

        [GenerateProperty]
        private string _UserName;

        [GenerateCommand]
        private void Login() => Status = "User: " + UserName;

        private bool CanLogin() => !string.IsNullOrEmpty(UserName);
    }
}