namespace Playground.Runners;

internal class ContainerWithMostWaterRunner : RunnerBase
{
    public override string Description => "Find maximum water area between vertical lines";

    public override ValueTask Run(IUserPrompt prompt)
    {
        var exampleArrays = new[]
        {
            new[] { 1, 8, 6, 2, 5, 4, 8, 3, 7 },
            new[] { 1, 1 },
            new[] { 4, 3, 2, 1, 4 }
        };

        var height = prompt.PromptIntArrayOrChoice("Select or enter line heights:", exampleArrays);
        var result = MaxArea(height);

        Console.WriteLine($"Heights: [{string.Join(", ", height)}]");
        Console.WriteLine($"Maximum area: {result}");

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
