
namespace Playground.Runners;

internal class ClearConsole : RunnerBase
{
    public override string Description => "Clear the console screen";

    public override ValueTask Run(IUserPrompt prompt)
    {
        Console.Clear();
        Console.Beep();

        return ValueTask.CompletedTask;
    }
}
