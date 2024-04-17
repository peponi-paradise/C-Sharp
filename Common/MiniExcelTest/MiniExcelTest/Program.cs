using MiniExcelLibs;
using MiniExcelLibs.OpenXml;

namespace MiniExcelTest
{
    public record PersonBase
    {
        public ulong ID { get; init; }
        public string? Name { get; init; }
        public string? PhoneNumber { get; init; }
    }

    public sealed record Customer : PersonBase
    {
        public ulong SalesVolume { get; init; }
    }

    public record Employee : PersonBase
    {
        public uint Age { get; init; }
        public DateTime Join { get; init; }
    }

    public sealed record EmployeeWithDepartment : Employee
    {
        public string? Department { get; init; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string dataFile = "MiniExcel.xlsx";
            string dataPath = Path.Combine(Environment.CurrentDirectory, dataFile);

            DynamicQuery(dataPath);

            Console.WriteLine("----------------------------------------------------");

            DynamicQueryWithHeader(dataPath);

            Console.WriteLine("----------------------------------------------------");

            QueryWithType(dataPath);

            Console.WriteLine("----------------------------------------------------");

            QueryWithLINQ(dataPath);

            Console.WriteLine("----------------------------------------------------");

            QueryBySheetName(dataPath, nameof(Employee));

            Console.WriteLine("----------------------------------------------------");

            QueryWithMergedCells(dataPath, nameof(EmployeeWithDepartment.Department));

            Console.WriteLine("----------------------------------------------------");

            Save();
            SaveWithMultipleSheets();
            SaveWithTemplate();
        }

        static void DynamicQuery(string dataPath)
        {
            var datas = MiniExcel.Query(dataPath);

            foreach (var data in datas)
            {
                Console.WriteLine($"{data.A}, {data.B}, {data.C}, {data.D}");
            }
        }

        static void DynamicQueryWithHeader(string dataPath)
        {
            var datas = MiniExcel.Query(dataPath, true);

            foreach (var data in datas)
            {
                Console.WriteLine($"{data.ID}, {data.Name}, {data.PhoneNumber}, {data.SalesVolume}");
            }
        }

        static void QueryWithType(string dataPath)
        {
            var datas = MiniExcel.Query<Customer>(dataPath);

            foreach (var data in datas)
            {
                Console.WriteLine($"{data.ID}, {data.Name}, {data.PhoneNumber}, {data.SalesVolume}");
            }
        }

        static void QueryWithLINQ(string dataPath)
        {
            var datas = MiniExcel.Query<Customer>(dataPath).Where(x => x.SalesVolume >= 1015);

            foreach (var data in datas)
            {
                Console.WriteLine($"{data.ID}, {data.Name}, {data.PhoneNumber}, {data.SalesVolume}");
            }
        }

        static void QueryBySheetName(string dataPath, string sheetName)
        {
            var datas = MiniExcel.Query<Employee>(dataPath, sheetName);

            foreach (var data in datas)
            {
                Console.WriteLine($"{data.ID}, {data.Name}, {data.PhoneNumber}, {data.Age}, {data.Join}");
            }
        }

        static void QueryWithMergedCells(string dataPath, string sheetName)
        {
            var config = new OpenXmlConfiguration() { FillMergedCells = true };
            var datas = MiniExcel.Query<EmployeeWithDepartment>(dataPath, sheetName, configuration: config);

            foreach (var data in datas)
            {
                Console.WriteLine($"{data.Department}, {data.ID}, {data.Name}, {data.PhoneNumber}, {data.Age}, {data.Join}");
            }
        }

        static void Save()
        {
            List<Employee> employees = [];
            for (uint i = 0; i < 30; i++)
            {
                employees.Add(new() { ID = i, Name = i.ToString(), PhoneNumber = i.ToString("010-0000-0000"), Age = i + 20, Join = DateTime.Today.AddDays(i) });
            }

            string savePath = Path.Combine(Environment.CurrentDirectory, $"{nameof(Employee)}.xlsx");
            MiniExcel.SaveAs(savePath, employees);
        }

        static void SaveWithMultipleSheets()
        {
            List<Customer> customers = [];
            List<Employee> employees = [];
            for (uint i = 0; i < 30; i++)
            {
                customers.Add(new() { ID = i, Name = i.ToString(), PhoneNumber = i.ToString("010-0000-0000"), SalesVolume = 1000 + i });
            }
            for (uint i = 0; i < 30; i++)
            {
                employees.Add(new() { ID = i, Name = i.ToString(), PhoneNumber = i.ToString("010-0000-0000"), Age = i + 20, Join = DateTime.Today.AddDays(i) });
            }

            // 각 키마다 하나의 sheet로 저장
            var multipleSheets = new Dictionary<string, object>
            {
                [nameof(customers)] = customers,
                [nameof(employees)] = employees
            };

            string savePath = Path.Combine(Environment.CurrentDirectory, $"{nameof(multipleSheets)}.xlsx");
            MiniExcel.SaveAs(savePath, multipleSheets);
        }

        static void SaveWithTemplate()
        {
            List<Customer> customers = [];
            for (uint i = 0; i < 30; i++)
            {
                customers.Add(new() { ID = i, Name = i.ToString(), PhoneNumber = i.ToString("010-0000-0000"), SalesVolume = 1000 + i });
            }

            var saveObject = new
            {
                Customers = customers
            };

            string savePath = Path.Combine(Environment.CurrentDirectory, $"{nameof(Customer)}.xlsx");
            string templatePath = Path.Combine(Environment.CurrentDirectory, $"{nameof(Customer)}Template.xlsx");
            MiniExcel.SaveAsByTemplate(savePath, templatePath, saveObject);
        }
    }
}