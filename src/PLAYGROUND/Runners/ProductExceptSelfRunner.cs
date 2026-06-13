namespace Playground.Runners;

internal class ProductExceptSelfRunner : RunnerBase
{
    public override string Description => "XXXXX";

    public override ValueTask Run(IUserPrompt prompt)
    {
        return ValueTask.CompletedTask;
    }

    public int[] ProductExceptSelf(int[] nums)
    {
        return nums;
    }
}
