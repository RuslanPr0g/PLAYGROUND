namespace Playground.Runners;

public class ListNode(int val = 0, ListNode? next = null)
{
  public int val = val;
  public ListNode? next = next;
}

internal class ReorderListRunner : RunnerBase
{
  public override string Description => "ddddd";

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

  public override ValueTask Run(IUserPrompt prompt)
  {
    return ValueTask.CompletedTask;
  }
}
