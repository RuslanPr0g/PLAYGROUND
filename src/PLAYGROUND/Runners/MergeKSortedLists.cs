namespace Playground.Runners;

// TODO: use min heap / prio queue

public class ListNode(int val = 0, ListNode? next = null)
{
  public int val = val;
  public ListNode? next = next;
}

internal class MergeKListsRunner : RunnerBase
{
  public override string Description => "MergeKLists";

  public ListNode? MergeKLists(ListNode?[] lists)
  {
    var currMinNum = int.MaxValue;
    var currMinPos = -1;
    ListNode? currMinNode = null;
    ListNode start = new();
    ListNode? ptr = start;

    while (lists.Any(v => v is not null))
    {
      for (var i = 0; i < lists.Length; i++)
      {
        if (lists[i] is null)
        {
          continue;
        }

        if (lists[i]!.val < currMinNum)
        {
          currMinNode = lists[i];
          currMinPos = i;
          currMinNum = lists[i]!.val;
        }
      }

      ptr!.next = currMinNode;
      ptr = ptr.next;

      lists[currMinPos] = currMinNode!.next;

      currMinNum = int.MaxValue;
      currMinPos = -1;
    }

    return start.next;
  }

  public override ValueTask Run(IUserPrompt prompt)
  {
    MergeKLists([]);
    return ValueTask.CompletedTask;
  }
}
