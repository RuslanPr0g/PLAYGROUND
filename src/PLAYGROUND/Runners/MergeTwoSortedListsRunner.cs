namespace Playground.Runners;

public class ListNodeSort(int val = 0, ListNodeSort? next = null)
{
  public int val = val;
  public ListNodeSort? next = next;
}

internal class MergeTwoSortedListsRunner : RunnerBase
{
  public override string Description => "Merge two sorted linked lists into one sorted list";

  public override ValueTask Run(IUserPrompt prompt)
  {
    var exampleList1 = new[]
    {
      new[] { 1, 2, 4 },
      new[] { 1 },
      new[] { 0 }
    };
    var exampleList2 = new[]
    {
      new[] { 1, 3, 4 },
      new[] { 2, 3 },
      new[] { 1, 2, 3 }
    };

    var list1Values = prompt.PromptIntArrayOrChoice("Select or enter first sorted list:", exampleList1);
    var list2Values = prompt.PromptIntArrayOrChoice("Select or enter second sorted list:", exampleList2);

    var list1 = FromArray(list1Values);
    var list2 = FromArray(list2Values);
    var merged = MergeTwoLists(list1, list2);

    Console.WriteLine($"List 1: {FormatList(list1)}");
    Console.WriteLine($"List 2: {FormatList(list2)}");
    Console.WriteLine($"Merged: {FormatList(merged)}");

    return ValueTask.CompletedTask;
  }

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

  private static ListNodeSort? FromArray(int[] values)
  {
    if (values.Length == 0)
    {
      return null;
    }

    var head = new ListNodeSort(values[0]);
    var current = head;
    for (var i = 1; i < values.Length; i++)
    {
      current.next = new ListNodeSort(values[i]);
      current = current.next;
    }

    return head;
  }

  private static string FormatList(ListNodeSort? head)
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
