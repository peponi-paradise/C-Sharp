namespace Define.BootStrap;

public interface IBootStrap
{
    bool InitDataBase(string? defaultPath);

    bool InitStaticResources();

    void Run();
}