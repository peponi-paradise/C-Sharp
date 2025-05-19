using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace DynamicCompilation;

public class RunAsyncTest
{
    public List<double> Parameters;

    public string ComputationCode;

    private Script _script;

    public RunAsyncTest(List<double> parameters, string code)
    {
        Parameters = parameters;
        ComputationCode = CreateCode(code);

        _script = CreateScript();
    }

    public Task<object> RunScriptAsync(AdditionalData additional) => Task.Run(() => _script.RunAsync(additional).Result.ReturnValue);

    private string CreateCode(string code)
    {
        string parameters = string.Empty;
        for (int i = 0; i < Parameters.Count; i++)
        {
            parameters += $"double P{i + 1} = {Parameters[i]};";
        }
        return parameters + code;
    }

    private Script CreateScript()
    {
        var options = ScriptOptions.Default;
        options = options.AddImports("System");

        var script = CSharpScript.Create(ComputationCode, options, typeof(AdditionalData));
        script.Compile();

        return script;
    }
}

public record AdditionalData(int X, double Y);

internal class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("Start");
        Console.WriteLine();

        var runAsync = new RunAsyncTest([1, 2, 3], GetRunAsyncCode());
        var sum = await runAsync.RunScriptAsync(new(10, 7));

        Console.WriteLine($"RunAsync() result : {sum}");

        var evaluateAsync = await CSharpScript.EvaluateAsync(GetEvaluateAsyncCode(), globals: new AdditionalData(5, 7));

        Console.WriteLine($"EvaluateAsync() result : {evaluateAsync}");

        Console.WriteLine();
        Console.WriteLine("End");
    }

    private static string GetRunAsyncCode()
    {
        return "return P1 + Math.Pow(P2, P3) + X - Y;";
    }

    private static string GetEvaluateAsyncCode()
    {
        return "return X - Y;";
    }
}