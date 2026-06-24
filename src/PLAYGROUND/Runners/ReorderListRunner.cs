namespace Playground.Runners;

public class ListNode(int val = 0, ListNode? next = null)
{
  public int val = val;
  public ListNode? next = next;
}

internal class ReorderListRunner : RunnerBase
{
  public override string Description => "Reorder list as L0→Ln→L1→Ln-1→...";

  public override ValueTask Run(IUserPrompt prompt)
  {
    var exampleArrays = new[]
    {
      new[] { 1, 2, 3, 4 },
      new[] { 1, 2, 3, 4, 5 },
      new[] { 1, 2 }
    };

    var values = prompt.PromptIntArrayOrChoice("Select or enter linked list values:", exampleArrays);
    var head = FromArray(values);

    Console.WriteLine($"Input:  {FormatList(head)}");
    ReorderList(head!);
    Console.WriteLine($"Output: {FormatList(head)}");

    return ValueTask.CompletedTask;
  }

  public void ReorderList(ListNode head)
  {
    var slow = head;
    var fast = head.next;

    while (fast?.next is not null)
    {
      slow = slow!.next;
      fast = fast.next.next;
    }

    var middleStart = slow!.next;
    slow.next = null;
    var reversed = ReverseList(middleStart);
    var reversedIterator = reversed;

    var reorderIterator = head;

    while (reversedIterator is not null)
    {
      var step = reorderIterator!.next;
      reorderIterator.next = reversedIterator;
      reversedIterator = reversedIterator!.next;
      reorderIterator.next?.next = step;
      reorderIterator = step;
    }
  }

  public ListNode? ReverseList(ListNode? head)
  {
    ListNode? prev = null;

    while (head is not null)
    {
      var next = head.next;

      head.next = prev;
      prev = head;
      head = next;
    }

    return prev;
  }

  private static ListNode? FromArray(int[] values)
  {
    if (values.Length == 0)
    {
      return null;
    }

    var head = new ListNode(values[0]);
    var current = head;
    for (var i = 1; i < values.Length; i++)
    {
      current.next = new ListNode(values[i]);
      current = current.next;
    }

    return head;
  }

  private static string FormatList(ListNode? head)
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
