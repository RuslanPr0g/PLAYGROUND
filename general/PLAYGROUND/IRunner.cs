namespace Playground;

internal interface IRunner
{
    public string Name { get; }

    ValueTask Run();
}
