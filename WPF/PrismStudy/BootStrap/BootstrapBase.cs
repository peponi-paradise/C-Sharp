using Define.Bootstrap;
using ModelLib.Bootstrap;

namespace Bootstrap;

public class BootstrapBase
{
    private IBootstrap Bootstrapper { get; set; }

    public BootstrapBase() => InitBootstrapper(null);

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
            //try
            // var infrastructure=YAML file binding
            // return infrastructure;

            //catch
            //no file.
            //return null;
        }
        return null;
    }

    public void Run() => Bootstrapper.Run();
}