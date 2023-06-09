namespace Define.Bootstrap;

public interface IBootstrap
{
    bool InitDataBase(string? defaultPath);

    bool InitStaticResources();

    void Run();
}