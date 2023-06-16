using BootStrap.BootStrapper;
using Common.Utility.Parser;
using Define.BootStrap;
using System;

namespace BootStrap;

public class BootStrapBase
{
    private IBootStrap? BootStrapper { get; set; }

    public BootStrapBase() => InitBootstrapper($@"{AppDomain.CurrentDomain.BaseDirectory}\Config\Infrastructure.yaml");

    private void InitBootstrapper(string? dataPath)
    {
        // If application configuration file exists, load here
        var infrastructure = LoadInfrastructure(dataPath);
        if (infrastructure == null) BootStrapper = new BaseBootStrapper();
        else
        {
            BootStrapper = infrastructure.ApplicationType switch
            {
                _ => new BaseBootStrapper(),
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

    public void Run() => BootStrapper?.Run();
}