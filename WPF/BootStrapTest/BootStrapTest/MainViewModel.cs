using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootStrap.Interfaces;
using DevExpress.Mvvm.CodeGenerators;

namespace BootStrapTest
{
    [GenerateViewModel]
    public partial class MainViewModel : IViewModel
    {
        [GenerateProperty]
        public string _Texting = "Hello World!";
    }
}