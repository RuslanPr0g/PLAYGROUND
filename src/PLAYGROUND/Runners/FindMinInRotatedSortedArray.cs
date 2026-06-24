namespace Playground.Runners;

internal class FindMinimumInRotatedArray : RunnerBase
{
  public override string Description => "Find the minimum element in a rotated sorted array";

  public override ValueTask Run(IUserPrompt prompt)
  {
    var exampleArrays = new[]
    {
      new[] { 3, 4, 5, 1, 2 },
      new[] { 4, 5, 6, 7, 0, 1, 2 },
      new[] { 11, 13, 15, 17 }
    };

    var nums = prompt.PromptIntArrayOrChoice("Select or enter the rotated sorted array:", exampleArrays);
    var minIndex = FindMin(nums);

    Console.WriteLine($"Array:   [{string.Join(", ", nums)}]");
    Console.WriteLine($"Minimum: {nums[minIndex]} (index {minIndex})");

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
