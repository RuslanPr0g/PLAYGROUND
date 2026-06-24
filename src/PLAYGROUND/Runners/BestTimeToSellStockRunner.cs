namespace Playground.Runners;

internal class BestTimeToSellStockRunner : RunnerBase
{
  public override string Description => "BestTimeToSellStockRunner";

  public override ValueTask Run(IUserPrompt prompt)
  {
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
