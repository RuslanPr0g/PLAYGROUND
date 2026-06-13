using System.Text;

namespace Playground.Runners;

internal class EncodeDecodeRunner : RunnerBase
{
    public override string Description => "Encode list of strings to single string and decode back";

    public override ValueTask Run(IUserPrompt prompt)
    {
        var exampleInputs = new[]
        {
            "Hello,World",
            "code,love,you",
            "we,say,:,yes"
        };

        var input = prompt.PromptStringOrChoice("Select or enter strings (comma-separated):", exampleInputs);
        var strs = input.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();

        var encoded = Encode(strs);
        var decoded = Decode(encoded);

        Console.WriteLine($"Input:   [{string.Join(", ", strs)}]");
        Console.WriteLine($"Encoded: {encoded.Replace("\n", "\\n")}");
        Console.WriteLine($"Decoded: [{string.Join(", ", decoded)}]");
        Console.WriteLine($"Match:   {strs.SequenceEqual(decoded)}");

        return ValueTask.CompletedTask;
    }

    public string Encode(IList<string> strs)
    {
        var sb = new StringBuilder();

        foreach (var str in strs)
        {
            sb.Append(str);
            sb.Append('\n');
        }

        return sb.ToString();
    }

    public List<string> Decode(string s)
    {
        ReadOnlySpan<char> chars = s;
        var sb = new StringBuilder();
        var strs = new List<string>();

        foreach (var c in chars)
        {
            if (c == '\n')
            {
                strs.Add(sb.ToString());
                sb.Clear();
                continue;
            }

            sb.Append(c);
        }

        return strs;
    }
}
