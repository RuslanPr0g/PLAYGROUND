namespace Playground.Runners;

public class ListNodeReserse(int val = 0, ListNodeReserse? next = null)
{
  public int val = val;
  public ListNodeReserse? next = next;
}

internal sealed class ReverseLinkedListRunner : RunnerBase
{
  public override string Description => "Reverse a singly linked list";

  public override ValueTask Run(IUserPrompt prompt)
  {
    var exampleArrays = new[]
    {
      new[] { 1, 2, 3, 4, 5 },
      new[] { 1, 2 },
      new[] { 1 }
    };

    var values = prompt.PromptIntArrayOrChoice("Select or enter linked list values:", exampleArrays);
    var head = FromArray(values);

    Console.WriteLine($"Input:  {FormatList(head)}");

    var reversed = ReverseList(head);

    Console.WriteLine($"Output: {FormatList(reversed)}");

    return ValueTask.CompletedTask;
  }

  public ListNodeReserse? ReverseList(ListNodeReserse? head)
  {
    ListNodeReserse? prev = null;

    while (head is not null)
    {
      var next = head.next;

      head.next = prev;
      prev = head;
      head = next;
    }

    return prev;
  }

  private static ListNodeReserse? FromArray(int[] values)
  {
    if (values.Length == 0)
    {
      return null;
    }

    var head = new ListNodeReserse(values[0]);
    var current = head;
    for (var i = 1; i < values.Length; i++)
    {
      current.next = new ListNodeReserse(values[i]);
      current = current.next;
    }

    return head;
  }

  private static string FormatList(ListNodeReserse? head)
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
