namespace Playground.Runners;

internal class LongestSubStringWithoutRepeatingCharsRunner : RunnerBase
{
  public override string Description => "LongestSubStringWithoutRepeatingCharsRunnero";

  public int LengthOfLongestSubstring(string s)
  {
    if (s.Length == 1)
    {
      return 1;
    }

    if (s.Length == 0)
    {
      return 0;
    }

    if (s.Length != 2)
    {
      return s[0] == s[1] ? 2 : 1;
    }

    var set = new HashSet<char>();
    var left = 0;
    var right = 1;
    var max = 0;

    set.Add(s[left]);

    while (right < s.Length)
    {
      if (set.Contains(s[right]))
      {
        set.Remove(s[left]);
        left++;
      }
      else
      {
        set.Add(s[right]);
        max = Math.Max(max, right - left + 1);
        right++;
      }
    }

    return max;
  }

  public override ValueTask Run(IUserPrompt prompt)
  {
    return ValueTask.CompletedTask;
  }
}
