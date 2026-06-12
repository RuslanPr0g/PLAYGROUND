
namespace Playground.Runners;

internal class TwoSumRunner : RunnerBase
{
    public override string Description => "Find two numbers that add up to target";

    public override ValueTask Run(IUserPrompt prompt)
    {
        var exampleArrays = new[]
        {
            new[] { 2, 7, 11, 15 },
            new[] { 3, 2, 4 },
            new[] { 3, 3 }
        };
        
        var nums = prompt.PromptIntArrayOrChoice("Select or enter the array of numbers:", exampleArrays);
        var target = prompt.PromptInt("Enter the target sum:");
        
        var result = TwoSum(nums, target);
        
        Console.WriteLine($"Indices: [{string.Join(", ", result)}]");
        Console.WriteLine($"Values: [{string.Join(", ", result.Select(i => nums[i]))}]");
        
        return ValueTask.CompletedTask;
    }

    public int[] TwoSum(int[] nums, int target)
    {
        var values = new Dictionary<int, int>();

        for (int i = 0; i < nums.Length; i++)
        {
            var value = nums[i];

            if (!values.TryAdd(value, i))
            {
                if (values.TryGetValue(target - value, out var index))
                {
                    return [i, index];
                }
            }
        }

        for (int i = 0; i < nums.Length; i++)
        {
            var value = nums[i];

            if (values.TryGetValue(target - value, out var index) && i != index)
            {
                return [i, index];
            }
        }

        return [];
    }
}
