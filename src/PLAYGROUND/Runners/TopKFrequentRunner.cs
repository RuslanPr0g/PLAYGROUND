namespace Playground.Runners;

internal class TopKFrequentRunner : RunnerBase
{
    public override string Description => "Find the k most frequently occurring elements";

    public override ValueTask Run(IUserPrompt prompt)
    {
        int[][] exampleInputs =
        [
            [1, 2, 1, 2, 1, 2, 3, 1, 3, 2],
            [1, 1, 1, 2, 2, 3],
            [4, 4, 4, 5, 5, 6]
        ];

        var elements = prompt.PromptIntArrayOrChoice("Select or enter the array of numbers:", exampleInputs);
        var k = prompt.PromptIntOrChoice("Select or enter k (top k frequent):", 2);

        var result = TopKFrequent(elements, k);

        Console.WriteLine($"Input: [{string.Join(", ", elements)}]");
        Console.WriteLine($"Top {k} frequent element(s): [{string.Join(", ", result)}]");

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
