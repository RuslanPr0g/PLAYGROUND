namespace Playground.Runners;

internal class IsPalindromeRunner : RunnerBase
{
    public override string Description => "XXXXX";

    public override ValueTask Run(IUserPrompt prompt)
    {
        IsPalindrome("A man, a plan, a canal: Panama");
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
