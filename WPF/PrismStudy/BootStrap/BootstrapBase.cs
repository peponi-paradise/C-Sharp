using Common.Utility.Parser;
using Define.Bootstrap;
using ModelLib.Bootstrap;
using System;

namespace Bootstrap;

public class BootstrapBase
{
    private IBootstrap? Bootstrapper { get; set; }

    public BootstrapBase() => InitBootstrapper($@"{AppDomain.CurrentDomain.BaseDirectory}\Infrastructure.yaml");

    private void InitBootstrapper(string? dataPath)
    {
        // If application configuration file exists, load here
        var infrastructure = LoadInfrastructure(dataPath);
        if (infrastructure == null) Bootstrapper = new BaseBootstrapper();
        else
        {
            Bootstrapper = infrastructure.ApplicationType switch
            {
                _ => new BaseBootstrapper(),
            };
        }
    }

    private Infrastructure? LoadInfrastructure(string? dataPath)
    {
        if (dataPath == null) return new Infrastructure();
        else
        {
            try
            {
                if (YAMLParser.LoadData(dataPath, out Infrastructure? data)) return data;
                else return null;
            }
            catch
            {
                return null;
            }
        }
    }

    public void Run() => Bootstrapper?.Run();
}