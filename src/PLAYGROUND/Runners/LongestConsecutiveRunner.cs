namespace Playground.Runners;

internal class LongestConsecutiveRunner : RunnerBase
{
    public override string Description => "XXXXX";

    public override ValueTask Run(IUserPrompt prompt)
    {
        LongestConsecutive([0, 3, 7, 2, 5, 8, 4, 6, 0, 1]);
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
