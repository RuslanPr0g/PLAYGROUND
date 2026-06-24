namespace Playground.Runners;

internal class ProductExceptSelfRunner : RunnerBase
{
    public override string Description => "Compute product of array except self without division";

    public override ValueTask Run(IUserPrompt prompt)
    {
        var exampleArrays = new[]
        {
            new[] { 1, 2, 3, 4 },
            new[] { -1, 1, 0, -3, 3 },
            new[] { 2, 3 }
        };

        var input = prompt.PromptIntArrayOrChoice("Select or enter the array of numbers:", exampleArrays);
        var nums = (int[])input.Clone();
        var result = ProductExceptSelf(nums);

        Console.WriteLine($"Input:  [{string.Join(", ", input)}]");
        Console.WriteLine($"Output: [{string.Join(", ", result)}]");

        return ValueTask.CompletedTask;
    }

    public int[] ProductExceptSelf(int[] nums)
    {
        var left = new int[nums.Length];
        var right = new int[nums.Length];
        var accumulator = 1;

        left[0] = 1;
        right[nums.Length - 1] = 1;

        for (var i = 1; i < nums.Length; i++)
        {
            left[i] = accumulator * nums[i - 1];
            accumulator *= nums[i - 1];
        }

        accumulator = 1;

        for (var i = nums.Length - 2; i >= 0; i--)
        {
            right[i] = accumulator * nums[i + 1];
            accumulator *= nums[i + 1];
        }

        for (var i = 0; i < nums.Length; i++)
        {
            nums[i] = left[i] * right[i];
        }

        return nums;
    }
}
