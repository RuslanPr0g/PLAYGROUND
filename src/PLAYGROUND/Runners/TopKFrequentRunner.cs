namespace Playground.Runners;

internal class TopKFrequentRunner : RunnerBase
{
    public override string Description => "XXXXX";

    public override ValueTask Run(IUserPrompt prompt)
    {
        int[][] exampleInputs =
        [
            [1, 2, 1, 2, 1, 2, 3, 1, 3, 2]
        ];

        var elements = prompt.PromptIntArrayOrChoice("Select or enter strings (comma-separated):", exampleInputs);
        var integer = prompt.PromptIntOrChoice("Select or enter top k elements:", 2);

        var result = TopKFrequent(elements, integer);

        Console.WriteLine("Anagram groups:");
        foreach (var group in result)
        {
            Console.WriteLine($"  [{string.Join(", ", group)}]");
        }

        return ValueTask.CompletedTask;
    }

    public int[] TopKFrequent(int[] nums, int k)
    {
        var dict = new Dictionary<int, int>();

        for (int i = 0; i < nums.Length; i++)
        {
            if (dict.TryGetValue(nums[i], out var count))
            {
                dict[nums[i]] = count + 1;
            }
            else
            {
                dict[nums[i]] = 1;
            }
        }

        return [.. dict.OrderByDescending(x => x.Value).Take(k).Select(x => x.Key)];
    }
}
