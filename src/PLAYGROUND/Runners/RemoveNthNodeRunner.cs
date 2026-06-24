namespace Playground.Runners;

public class ListNodeRemoveNth(int val = 0, ListNodeRemoveNth? next = null)
{
  public int val = val;
  public ListNodeRemoveNth? next = next;
}

internal class RemoveNthFromEndRunner : RunnerBase
{
  public override string Description => "Remove the nth node from the end of a linked list";

  public override ValueTask Run(IUserPrompt prompt)
  {
    var exampleArrays = new[]
    {
      new[] { 1, 2, 3, 4, 5 },
      new[] { 1 },
      new[] { 1, 2 }
    };

    var values = prompt.PromptIntArrayOrChoice("Select or enter linked list values:", exampleArrays);
    var n = prompt.PromptIntOrChoice("Select or enter n (remove nth from end):", 2);
    var head = FromArray(values);

    Console.WriteLine($"Input:  {FormatList(head)}");
    Console.WriteLine($"Remove: {n} node(s) from end");

    var result = RemoveNthFromEndV2(head!, n);

    Console.WriteLine($"Output: {FormatList(result)}");

    return ValueTask.CompletedTask;
  }

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

  private static ListNodeRemoveNth? FromArray(int[] values)
  {
    if (values.Length == 0)
    {
      return null;
    }

    var head = new ListNodeRemoveNth(values[0]);
    var current = head;
    for (var i = 1; i < values.Length; i++)
    {
      current.next = new ListNodeRemoveNth(values[i]);
      current = current.next;
    }

    return head;
  }

  private static string FormatList(ListNodeRemoveNth? head)
  {
    var values = new List<int>();
    while (head is not null)
    {
      values.Add(head.val);
      head = head.next;
    }

    return values.Count == 0 ? "(empty)" : string.Join(" → ", values);
  }
}
