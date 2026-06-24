namespace Playground.Runners;

internal class LongestConsecutiveRunner : RunnerBase
{
    public override string Description => "Find length of longest consecutive integer sequence";

    public override ValueTask Run(IUserPrompt prompt)
    {
        var exampleArrays = new[]
        {
            new[] { 100, 4, 200, 1, 3, 2 },
            new[] { 0, 3, 7, 2, 5, 8, 4, 6, 0, 1 },
            new[] { 1, 2, 0, 1 }
        };

        var nums = prompt.PromptIntArrayOrChoice("Select or enter the array of numbers:", exampleArrays);
        var result = LongestConsecutive(nums);

        Console.WriteLine($"Input:  [{string.Join(", ", nums)}]");
        Console.WriteLine($"Longest consecutive sequence length: {result}");

        return ValueTask.CompletedTask;
    }

    public int LongestConsecutive(int[] nums)
    {
        var set = nums.ToHashSet();
        var max = 0;

        foreach (var item in set)
        {
            if (set.Contains(item - 1))
            {
                continue;
            }

            var acc = 0;
            var itenc = item;

            while (set.Contains(itenc++))
            {
                acc++;
            }

            max = Math.Max(max, acc);
        }

        return max;
    }
}
