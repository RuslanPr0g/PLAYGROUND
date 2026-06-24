namespace Playground.Runners;

internal class IsPalindromeRunner : RunnerBase
{
    public override string Description => "Check if a string is a palindrome (alphanumeric only)";

    public override ValueTask Run(IUserPrompt prompt)
    {
        var stringExamples = new[]
        {
            "A man, a plan, a canal: Panama",
            "race a car",
            " "
        };

        var s = prompt.PromptStringOrChoice("Select or enter a string to check:", stringExamples);
        var result = IsPalindrome(s);

        Console.WriteLine($"Input:       \"{s}\"");
        Console.WriteLine($"Is palindrome: {result}");

        return ValueTask.CompletedTask;
    }

    public bool IsPalindrome(string s)
    {
        for (int i = 0, j = s.Length - 1; i < s.Length && j >= i;)
        {
            var a = char.ToLower(s[i]);
            var b = char.ToLower(s[j]);

            if (!char.IsLetterOrDigit(a))
            {
                i++;
                continue;
            }

            if (!char.IsLetterOrDigit(b))
            {
                j--;
                continue;
            }

            if (a != b)
            {
                return false;
            }

            i++;
            j--;
        }

        return true;
    }
}
