namespace Playground.Runners;

public class ListNodeReserse(int val = 0, ListNodeReserse? next = null)
{
  public int val = val;
  public ListNodeReserse? next = next;
}

internal sealed class ReverseLinkedListRunner : RunnerBase
{
  public override string Description => "XXXX";

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

  public override ValueTask Run(IUserPrompt prompt)
  {
    ReverseList(new());
    return ValueTask.CompletedTask;
  }
}
