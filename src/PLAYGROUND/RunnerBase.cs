namespace Playground;

internal abstract class RunnerBase : IRunner
{
    public string Name => GetType().Name.Replace("Runner", "");
    
    public abstract string Description { get; }

    public abstract ValueTask Run(IUserPrompt prompt);
}
