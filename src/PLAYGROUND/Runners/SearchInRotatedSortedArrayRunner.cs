namespace Playground.Runners;

internal class SearchInRotatedSortedArrayRunner : RunnerBase
{
  public override string Description => "SearchInRotatedSortedArrayRunner";

  public override ValueTask Run(IUserPrompt prompt)
  {
    return ValueTask.CompletedTask;
  }

  public int Search(int[] nums, int target)
  {
    var left = 0;
    var right = nums.Length - 1;

    if (nums.Length == 0)
    {
      return -1;
    }

    while (left < right)
    {
      var middle = (left + right) / 2;
      var leftElement = nums[left];
      var rightElement = nums[right];
      var middleElement = nums[middle];

      if (target == middleElement)
      {
        return middle;
      }

      if (leftElement <= middleElement)
      {
        // left side is sorted
        if (leftElement <= target && target < middleElement)
        {
          // the target must be on the left side
          right = middle - 1;
        }
        else
        {
          // the target must be on the right side
          left = middle + 1;
        }
      }
      else
      {
        //right side is sorted
        if (middleElement < target && target <= rightElement)
        {
          // the target must be on the right side
          left = middle + 1;
        }
        else
        {
          // the target must beon the left side
          right = middle - 1;
        }
      }
    }

    return nums[left] == target ? left : -1;
  }
}
