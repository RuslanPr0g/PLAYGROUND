namespace Playground.Runners;

internal class ContainerWithMostWaterRunner : RunnerBase
{
    public override string Description => "XXXXX";

    public override ValueTask Run(IUserPrompt prompt)
    {
        Console.WriteLine(MaxArea([1, 8, 6, 2, 5, 4, 8, 3, 7]));
        return ValueTask.CompletedTask;
    }

    public int MaxArea(int[] height)
    {
        var max = 0;

        for (int i = 0, j = height.Length - 1; i < height.Length && i < j;)
        {
            var volume = Math.Min(height[i], height[j]) * (j - i);

            max = Math.Max(max, volume);

            if (height[i] == height[j])
            {
                if (height[i + 1] > height[j - 1])
                {
                    i++;
                }
                else
                {
                    j--;
                }
            }

            if (height[i] < height[j])
            {
                i++;
            }
            else
            {
                j--;
            }
        }

        return max;
    }
}
