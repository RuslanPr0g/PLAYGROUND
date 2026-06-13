namespace Playground.Runners;

internal class TemplateRunner : RunnerBase
{
    public override string Description => "XXXXX";

    public override ValueTask Run(IUserPrompt prompt)
    {
        var exampleInputs = new[]
        {
            "eat,tea,tan,ate,nat,bat",
            "abc,bac,cab,def,fed",
            "a"
        };

        var input = prompt.PromptStringOrChoice("Select or enter strings (comma-separated):", exampleInputs);
        var strs = input.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();

        var result = Do(strs);

        Console.WriteLine("Anagram groups:");
        foreach (var group in result)
        {
            Console.WriteLine($"  [{string.Join(", ", group)}]");
        }

        return ValueTask.CompletedTask;
    }

    public IList<IList<string>> Do(string[] strs)
    {
        var map = new Dictionary<string, IList<string>>();

        foreach (var s in strs)
        {
            var key = string.Concat(s.OrderBy(c => c));
            if (!map.TryGetValue(key, out var group))
            {
                group = new List<string>();
                map[key] = group;
            }
            group.Add(s);
        }

        return [.. map.Values];
    }
}
