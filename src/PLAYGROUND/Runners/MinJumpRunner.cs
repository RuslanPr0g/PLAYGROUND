
namespace Playground.Runners;

internal sealed class MinJumpRunner : RunnerBase
{
    public override string Description => "Find minimum jumps to reach end";

    public override ValueTask Run(IUserPrompt prompt)
    {
        var exampleArrays = new[]
        {
            new[] { 2, 3, 1, 1, 4 },
            new[] { 2, 3, 0, 1, 4 },
            new[] { 1, 1, 1, 1 }
        };
        
        var nums = prompt.PromptIntArrayOrChoice("Select or enter the array of jump values:", exampleArrays);
        
        var result = new Solution().CanJump(nums);
        
        Console.WriteLine($"Minimum jumps to reach end: {result}");
        
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
