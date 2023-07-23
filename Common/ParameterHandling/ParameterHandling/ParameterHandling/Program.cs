using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterHandling
{
    internal class Program
    {
        public class Parameter
        {
            public string Name = default;
            public bool Value1 = false;
            public double Value2 = 0;
            public int Value3 = 0;
        }

        static void Main(string[] args)
        {
            Parameter parameter = new Parameter();
            Parameter parameter2 = new Parameter();
            parameter2.Name = "parameter2";
            ParameterHandling.CopyAllFieldsAndProperties(parameter, parameter2);
            string a = "";
            ParameterHandling.GetParameter(nameof(parameter.Name), ref a, parameter);
            ParameterHandling.SetParameter(nameof(parameter.Value2), 0.1213, parameter);
        }
    }
}