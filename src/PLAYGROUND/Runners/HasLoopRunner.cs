namespace Playground.Runners;

public class ListNodeCycle(int x)
{
  public int val = x;
  public ListNodeCycle? next = null;
}

internal class HasLoopRunner : RunnerBase
{
  public override string Description => "has cycle";

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

  public override ValueTask Run(IUserPrompt prompt)
  {
    HasCycle(new(4));
    return ValueTask.CompletedTask;
  }
}
