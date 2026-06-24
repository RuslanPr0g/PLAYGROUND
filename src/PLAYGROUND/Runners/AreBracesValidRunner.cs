namespace Playground.Runners;

internal partial class AreBracesValidRunner : RunnerBase
{
    public override string Description => "Validate matching parentheses, brackets, and braces";

    public override ValueTask Run(IUserPrompt prompt)
    {
        var stringExamples = new[]
        {
            "()[]{}",
            "(]",
            "([)]",
            "{[]}",
            "((("
        };

        var s = prompt.PromptStringOrChoice("Select or enter a string of brackets to validate:", stringExamples);
        var result = IsValid(s);

        Console.WriteLine($"Input:  \"{s}\"");
        Console.WriteLine($"Valid:  {result}");

        return ValueTask.CompletedTask;
    }

    public bool IsValid(string s)
    {
        var stack = DataStructures.Stack<char>.Create();

        foreach (var item in s)
        {
            if (IsOpening(item))
            {
                stack.Add(item);
                continue;
            }

            if (IsOfSameType(stack.Peek(), item))
            {
                stack.Pop();
            }
            else
            {
                return false;
            }
        }

        return stack.Head < 0;
    }

    private bool IsOpening(char c) => c == '[' || c == '(' || c == '{';

    private bool IsOfSameType(char opening, char closing) => (closing == ']' && opening == '[') || (closing == '}' && opening == '{') || (closing == ')' && opening == '(');
}
