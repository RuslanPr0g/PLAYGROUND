
namespace Playground.Runners;

internal sealed class MinJumpRunner : IRunner
{
    public string Name => nameof(MinJumpRunner);

    public ValueTask Run()
    {
        new Solution().CanJump([10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 1, 0]);

        new Solution().CanJump([1, 1, 1, 1]);

        var res1 = new Solution().CanJump([2, 3, 1, 1, 4]);
        var res2 = new Solution().CanJump([2, 3, 0, 1, 4]);

        Console.WriteLine(res1);
        Console.WriteLine(res1 == 2);
        Console.WriteLine(res2);
        Console.WriteLine(res2 == 2);

        return ValueTask.CompletedTask;
    }

    public class Solution
    {
        public int Jump(int[] nums)
        {
            return CanJump(nums);
        }

        public int CanJump(int[] nums)
        {
            int currIndex = 0;
            int tries = 0;

            while (currIndex < nums.Length - 1)
            {
                tries++;
                int maxReachIndex = currIndex + nums[currIndex];

                if (maxReachIndex >= nums.Length - 1)
                {
                    return tries;
                }

                var greaterIndex = FindGreaterValueInRange(currIndex, maxReachIndex, nums);

                currIndex = currIndex == greaterIndex ? maxReachIndex : greaterIndex;
            }

            return tries;
        }

        private int FindGreaterValueInRange(int index, int maxReachIndex, int[] nums)
        {
            int maxReach = int.MinValue;
            int maxIndex = -1;

            for (int i = maxReachIndex; i > index; i--)
            {
                int currReach = i + nums[i];

                if (currReach > maxReach)
                {
                    maxIndex = i;
                    maxReach = currReach;
                }
            }

            return maxIndex;
        }
    }
}
