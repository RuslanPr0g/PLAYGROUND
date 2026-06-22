namespace Playground.Runners;

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

  public ListNodeRemoveNth RemoveNthFromEndV2(ListNodeRemoveNth head, int n)
  {
    ListNodeRemoveNth dummy = new ListNodeRemoveNth(0, head);
    ListNodeRemoveNth start = dummy;
    ListNodeRemoveNth? end = dummy;

    for (var i = 0; i <= n; i++)
    {
      end = end?.next;
    }

    while (end is not null)
    {
      start = start.next!;
      end = end.next;
    }

    start.next = start.next?.next;

    return dummy.next!;
  }

  public override ValueTask Run(IUserPrompt prompt)
  {
    RemoveNthFromEnd(new(), 1);
    return ValueTask.CompletedTask;
  }
}
