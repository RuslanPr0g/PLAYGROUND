namespace Playground.Runners;

public class ListNode(int val = 0, ListNode? next = null)
{
  public int val = val;
  public ListNode? next = next;
}

internal sealed class ReverseLinkedListRunner : RunnerBase
{
  public override string Description => "XXXX";

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
    ReverseList(new());
    return ValueTask.CompletedTask;
  }
}
