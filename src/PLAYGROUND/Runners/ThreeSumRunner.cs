namespace Playground.Runners;

internal class ThreeSumRunner : RunnerBase
{
    public override string Description => "Find all unique triplets that sum to zero";

    public override ValueTask Run(IUserPrompt prompt)
    {
        var exampleArrays = new[]
        {
            new[] { -1, 0, 1, 2, -1, -4 },
            new[] { 0, 1, 1 },
            new[] { 0, 0, 0 }
        };

        var nums = prompt.PromptIntArrayOrChoice("Select or enter the array of numbers:", exampleArrays);
        var result = ThreeSum((int[])nums.Clone());

        Console.WriteLine($"Input: [{string.Join(", ", nums)}]");
        Console.WriteLine($"Triplets that sum to zero ({result.Count} found):");
        if (result.Count == 0)
        {
            Console.WriteLine("  (none)");
        }
        else
        {
            foreach (var triplet in result)
            {
                Console.WriteLine($"  [{string.Join(", ", triplet)}]");
            }
        }

        return ValueTask.CompletedTask;
    }

    public IList<IList<int>> ThreeSum(int[] nums)
    {
        var result = new List<IList<int>>();
        nums.Sort();

        for (int i = 0; i < nums.Length - 2; i++)
        {
            if (i > 0 && nums[i] == nums[i - 1])
                continue;

            var left = i + 1;
            var right = nums.Length - 1;

            while (left < right)
            {
                var sum = nums[i] + nums[left] + nums[right];
                if (sum == 0)
                {
                    result.Add([nums[i], nums[left], nums[right]]);

                    while (left < right && nums[left] == nums[left + 1])
                        left++;

                    while (left < right && nums[right] == nums[right - 1])
                        right--;

                    left++;
                    right--;
                }

                if (sum < 0) left++;

                if (sum > 0) right--;
            }
        }

        return result;
    }
}
