
namespace Playground.Runners;

internal class ClearConsole : IRunner
{
    public string Name => nameof(ClearConsole);

    public ValueTask Run()
    {
        Console.Clear();
        Console.Beep();

        return ValueTask.CompletedTask;
    }
}
