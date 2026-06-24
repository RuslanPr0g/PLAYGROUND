namespace Playground.Runners;

public class ListNodeCycle(int x)
{
  public int val = x;
  public ListNodeCycle? next = null;
}

internal class HasLoopRunner : RunnerBase
{
  public override string Description => "Detect whether a linked list has a cycle";

  public override ValueTask Run(IUserPrompt prompt)
  {
    var exampleArrays = new[]
    {
      new[] { 3, 2, 0, -4 },
      new[] { 1, 2 },
      new[] { 1 }
    };

    var values = prompt.PromptIntArrayOrChoice("Select or enter linked list values:", exampleArrays);
    var cycleIndex = prompt.PromptIntOrChoice(
      "Select or enter cycle start index (-1 for no cycle):",
      values.Length > 1 ? 1 : -1);

    var head = FromArrayWithCycle(values, cycleIndex);
    var result = HasCycle(head);

    Console.WriteLine($"Values:      [{string.Join(", ", values)}]");
    Console.WriteLine($"Cycle index: {(cycleIndex < 0 ? "none" : cycleIndex.ToString())}");
    Console.WriteLine($"Has cycle:   {result}");

    return ValueTask.CompletedTask;
  }

  public bool HasCycle(ListNodeCycle? head)
  {
    var dict = new Dictionary<ListNodeCycle, int>();
    var i = 0;

    while (head is not null)
    {
      if (dict.TryGetValue(head, out var val))
      {
        return val > -1;
      }

      dict.Add(head, i);
      i++;
      head = head.next;
    }

    return false;
  }

  private static ListNodeCycle? FromArrayWithCycle(int[] values, int cycleIndex)
  {
    if (values.Length == 0)
    {
      return null;
    }

    var nodes = new ListNodeCycle[values.Length];
    for (var i = 0; i < values.Length; i++)
    {
      nodes[i] = new ListNodeCycle(values[i]);
    }

    for (var i = 0; i < values.Length - 1; i++)
    {
      nodes[i].next = nodes[i + 1];
    }

    if (cycleIndex >= 0 && cycleIndex < values.Length)
    {
      nodes[^1].next = nodes[cycleIndex];
    }

    return nodes[0];
  }
}
