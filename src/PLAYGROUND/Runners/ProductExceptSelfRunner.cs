namespace Playground.Runners;

internal class ProductExceptSelfRunner : RunnerBase
{
    public override string Description => "XXXXX";

    public override ValueTask Run(IUserPrompt prompt)
    {
        ProductExceptSelf([1, 2, 3, 4]);
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
