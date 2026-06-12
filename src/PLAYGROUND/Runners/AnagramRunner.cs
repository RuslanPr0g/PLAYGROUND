
namespace Playground.Runners;

internal class AnagramRunner : RunnerBase
{
    public override string Description => "Check if two strings are anagrams";

    public override ValueTask Run(IUserPrompt prompt)
    {
        var stringExamples = new[] { "listen", "silent", "rat", "tar", "race", "care" };
        
        var s = prompt.PromptStringOrChoice("Select or enter first string:", stringExamples);
        var t = prompt.PromptStringOrChoice("Select or enter second string:", stringExamples);
        
        var result = IsAnagram(s, t);
        
        Console.WriteLine($"'{s}' and '{t}' are {(result ? "anagrams" : "not anagrams")}");
        
        return ValueTask.CompletedTask;
    }

    public bool IsAnagram(string s, string t)
    {
        var sTracker = new Dictionary<char, int>();
        var tTracker = new Dictionary<char, int>();

        for (int i = 0; i < s.Length; i++)
        {
            if (sTracker.TryGetValue(s[i], out int value))
            {
                sTracker[s[i]] = ++value;
            } else
            {
                sTracker.Add(s[i], 1);
            }
        }

        for (int i = 0; i < t.Length; i++)
        {
            if (tTracker.TryGetValue(t[i], out int value))
            {
                tTracker[t[i]] = ++value;
            }
            else
            {
                tTracker.Add(t[i], 1);
            }
        }

        if (sTracker.Count != tTracker.Count)
        {
            return false;
        }

        foreach (char c in sTracker.Keys)
        {
            sTracker.TryGetValue(c, out var sval);
            if (!tTracker.TryGetValue(c, out var tval) || sval != tval)
            {
                return false;
            }
        }

        return true;
    }
}
