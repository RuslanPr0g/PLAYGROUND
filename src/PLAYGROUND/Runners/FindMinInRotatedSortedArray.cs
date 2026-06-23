namespace Playground.Runners;

internal class FindMinimumInRotatedArray : RunnerBase
{
  public override string Description => "FindMinimumInRotatedArray";

  public override ValueTask Run(IUserPrompt prompt)
  {
    return ValueTask.CompletedTask;
  }

  public int FindMin(int[] nums)
  {
    var left = 0;
    var right = nums.Length - 1;
    var middle = (left + right) / 2;
    while (left < right)
    {
      var leftElement = nums[left];
      var rightElement = nums[right];
      var middleElement = nums[middle];
      if (middleElement > rightElement)
      {
        left = middle + 1;
        middle = (left + right) / 2;
        continue;
      }
      if (middleElement < leftElement)
      {
        right = middle;
        middle = (left + right) / 2;
        continue;
      }
    }
    return left;
  }
}
