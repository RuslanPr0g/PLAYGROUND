namespace Playground.Runners;

public class ListNodeMergeK(int val = 0, ListNodeMergeK? next = null)
{
  public int val = val;
  public ListNodeMergeK? next = next;
}

internal class MergeKListsRunner : RunnerBase
{
  public override string Description => "MergeKLists";

  public ListNodeMergeK? MergeKLists(ListNodeMergeK?[] lists)
  {
    var currMinNum = int.MaxValue;
    var currMinPos = -1;
    ListNodeMergeK? currMinNode = null;
    ListNodeMergeK start = new();
    ListNodeMergeK? ptr = start;

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

  public ListNodeMergeK MergeKListsV2(ListNodeMergeK[] lists)
  {
    var prio = new PriorityQueue<ListNodeMergeK, int>();
    var result = new ListNodeMergeK();
    var iterator = result;

    for (int i = 0; i < lists.Length; i++)
    {
      if (lists[i] is not null)
      {
        prio.Enqueue(lists[i], lists[i].val);
      }
    }

    while (prio.Count > 0)
    {
      var el = prio.Dequeue();
      iterator.next = el;
      iterator = iterator.next;

      if (el.next is not null)
      {
        prio.Enqueue(el.next, el.next.val);
      }
    }

    return result.next!;
  }

  public override ValueTask Run(IUserPrompt prompt)
  {
    MergeKLists([]);
    return ValueTask.CompletedTask;
  }
}
