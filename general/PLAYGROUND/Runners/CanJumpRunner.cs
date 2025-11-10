
namespace Playground.Runners;

internal sealed class CanJumpRunner : IRunner
{
    public string Name => nameof(CanJumpRunner);

    public ValueTask Run()
    {
        Console.WriteLine(new Solution().CanJump([2, 3, 1, 1, 4]));
        Console.WriteLine(new Solution().CanJump([3, 2, 1, 0, 4]));

        return ValueTask.CompletedTask;
    }

    public class Solution
    {
        public bool CanJump(int[] nums)
        {
            int currIndex = 0;

            while (currIndex < nums.Length - 1)
            {
                int maxReachIndex = currIndex + nums[currIndex];

                if (maxReachIndex >= nums.Length - 1)
                {
                    return true;
                }

                var greaterCurrIndex = FindGreaterValueInRange(currIndex, maxReachIndex, nums);

                if (currIndex != greaterCurrIndex)
                {
                    currIndex = greaterCurrIndex;
                    continue;
                }

                var maxReachValue = nums[maxReachIndex];

                if (maxReachValue != 0)
                {
                    currIndex = maxReachIndex;
                    continue;
                }

                var nextIndex = TryToJumpOverAGapAndGetNextIndex(currIndex, maxReachIndex, nums);

                if (nextIndex == -1)
                {
                    return false;
                }

                currIndex = nextIndex;
            }

            return true;
        }

        private int TryToJumpOverAGapAndGetNextIndex(int currIndex, int maxReachIndex, int[] nums)
        {
            for (int i = maxReachIndex - 1, j = 0; i > currIndex; i--, j++)
            {
                var currReachValue = nums[i];
                var currReachIndex = i + currReachValue;

                if (currReachIndex > maxReachIndex)
                {
                    return currReachIndex;
                }
            }

            return -1;
        }

        private int FindGreaterValueInRange(int index, int maxReachIndex, int[] nums)
        {
            int mainReach = nums[index];
            int maxIndex = index;

            for (int i = index + 1; i <= maxReachIndex; i++)
            {
                int currReach = nums[i];

                if (currReach > mainReach)
                {
                    maxIndex = i;
                }
            }

            return maxIndex;
        }
    }
}
