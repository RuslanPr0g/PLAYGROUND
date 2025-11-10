using Playground.Runners;
using System.Reflection;

namespace Playground;

internal class RunnerFactory
{
    private RunnerFactory()
    {
        
    }

    public static RunnerFactory Create()
    {
        return new RunnerFactory();
    }

    public List<IRunner> GetRunners()
    {
        var runnerTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(IRunner).IsAssignableFrom(t) && t.IsClass && !t.Name.Contains("Clear"))
            .ToList();

        var runners = runnerTypes.Select(type => (IRunner)Activator.CreateInstance(type)!).Where(x => x is not null).ToList();

        return [new ClearConsole(), ..runners];
    }
}
