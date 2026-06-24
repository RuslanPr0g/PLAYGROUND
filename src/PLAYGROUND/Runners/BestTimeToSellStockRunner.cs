namespace Playground.Runners;

internal class BestTimeToSellStockRunner : RunnerBase
{
  public override string Description => "Find maximum profit from one buy and one sell";

  public override ValueTask Run(IUserPrompt prompt)
  {
    var exampleArrays = new[]
    {
      new[] { 7, 1, 5, 3, 6, 4 },
      new[] { 7, 6, 4, 3, 1 },
      new[] { 2, 4, 1 }
    };

    var prices = prompt.PromptIntArrayOrChoice("Select or enter daily stock prices:", exampleArrays);
    var result = MaxProfix(prices);

    Console.WriteLine($"Prices: [{string.Join(", ", prices)}]");
    Console.WriteLine($"Maximum profit: {result}");

    return ValueTask.CompletedTask;
  }

  public int MaxProfix(int[] prices)
  {
    var max = 0;
    var minValue = int.MaxValue;

    for (var i = 0; i < prices.Length; i++)
    {
      if (prices[i] <= minValue)
      {
        minValue = prices[i];
        continue;
      }

      max = Math.Max(max, prices[i] - minValue);
    }

    return max;
  }
}
