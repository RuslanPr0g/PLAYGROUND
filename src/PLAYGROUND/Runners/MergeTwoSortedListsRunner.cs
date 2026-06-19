namespace Playground.Runners;

public class ListNodeSort(int val = 0, ListNodeSort? next = null)
{
  public int val = val;
  public ListNodeSort? next = next;
}

internal class MergeTwoSortedListsRunner : RunnerBase
{
  public override string Description => "XXXX";

  public ListNodeSort? MergeTwoLists(ListNodeSort? list1, ListNodeSort? list2)
  {
    ListNodeSort? dummy = new(0);
    ListNodeSort? merged = dummy;

    while (list1 is not null || list2 is not null)
    {
      if (list2 is null && list1 is not null)
      {
        merged.next = list1;
        list1 = list1.next;
        merged = merged.next;
        continue;
      }

      if (list1 is null && list2 is not null)
      {
        merged.next = list2;
        list2 = list2.next;
        merged = merged.next;
        continue;
      }

      var is1gt2 = list1?.val <= list2?.val;

      if (is1gt2)
      {
        merged.next = list1;
        list1 = list1?.next;
      }
      else
      {
        merged.next = list2;
        list2 = list2?.next;
      }

      merged = merged.next!;
    }

    return dummy.next;
  }

  public override ValueTask Run(IUserPrompt prompt)
  {
    MergeTwoLists(new(), new());
    return ValueTask.CompletedTask;
  }
}
