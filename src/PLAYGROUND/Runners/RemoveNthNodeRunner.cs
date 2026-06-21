namespace Playground.Runners;

// TODO: use the algo of a turtle and a rabbit (fast and slow pointers)

public class ListNodeRemoveNth(int val = 0, ListNodeRemoveNth? next = null)
{
  public int val = val;
  public ListNodeRemoveNth? next = next;
}

internal class RemoveNthFromEndRunner : RunnerBase
{
  public override string Description => "remove nth node";

  public ListNodeRemoveNth? RemoveNthFromEnd(ListNodeRemoveNth? head, int n)
  {
    var dict = new Dictionary<int, ListNodeRemoveNth>();
    int i = -1;
    var start = head;

    while (head is not null)
    {
      i++;
      dict.Add(i, head);
      head = head.next;
    }

    dict.TryGetValue(i - n, out var prev);
    dict.TryGetValue(i - n + 2, out var next);

    if (prev is null)
    {
      return next;
    }

    prev.next = next;

    return start;
  }

  public override ValueTask Run(IUserPrompt prompt)
  {
    RemoveNthFromEnd(new(), 1);
    return ValueTask.CompletedTask;
  }
}
