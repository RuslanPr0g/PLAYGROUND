namespace Playground;

internal interface IRunner
{
    public string Name { get; }
    
    public string Description { get; }

    ValueTask Run(IUserPrompt prompt);
}
