using Spectre.Console;

namespace Playground;

internal class SpectreConsolePrompt : IUserPrompt
{
    public string PromptString(string title)
    {
        return AnsiConsole.Ask<string>($"[bold]{title}[/]");
    }

    public int PromptInt(string title)
    {
        return AnsiConsole.Ask<int>($"[bold]{title}[/]");
    }

    public T PromptChoice<T>(string title, T[] choices) where T : notnull
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<T>()
                .Title($"[bold]{title}[/]")
                .AddChoices(choices)
        );
    }

    public bool Confirm(string title)
    {
        return AnsiConsole.Confirm($"[bold]{title}[/]");
    }

    public string PromptMultiLine(string title)
    {
        AnsiConsole.Markup($"[bold]{title}[/]");
        AnsiConsole.Markup("[dim] (Press Enter twice to finish)[/]\n");
        
        var lines = new List<string>();
        while (true)
        {
            var line = Console.ReadLine();
            if (string.IsNullOrEmpty(line))
            {
                if (lines.Count == 0 || string.IsNullOrEmpty(lines.Last()))
                    break;
            }
            lines.Add(line ?? "");
        }
        
        return string.Join("\n", lines.Where(l => !string.IsNullOrEmpty(l)));
    }

    public int[] PromptIntArray(string title)
    {
        var input = AnsiConsole.Ask<string>($"[bold]{title}[/] [dim](comma-separated, e.g., 1,2,3,4)[/]");
        return input.Split(',', StringSplitOptions.RemoveEmptyEntries)
                   .Select(s => int.Parse(s.Trim()))
                   .ToArray();
    }

    public string PromptStringOrChoice(string title, string[] examples)
    {
        var choices = new List<string> { "[yellow]Enter custom value[/]" };
        choices.AddRange(examples.Select((e, i) => $"[green]Example {i + 1}:[/] {e}"));

        var selected = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"[bold]{title}[/]")
                .AddChoices(choices)
        );

        if (selected.StartsWith("[yellow]Enter custom value[/]"))
        {
            return AnsiConsole.Ask<string>("[bold]Enter your value:[/]");
        }

        // Extract the example value (remove the prefix)
        var exampleIndex = int.Parse(selected.Split(':')[0].Replace("[green]Example ", "").Replace("[/]", "")) - 1;
        return examples[exampleIndex];
    }

    public int[] PromptIntArrayOrChoice(string title, int[][] examples)
    {
        var choices = new List<string> { "[yellow]Enter custom array[/]" };
        choices.AddRange(examples.Select((e, i) => $"[green]Example {i + 1}:[/] [[{string.Join(", ", e)}]]"));

        var selected = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"[bold]{title}[/]")
                .AddChoices(choices)
        );

        if (selected.StartsWith("[yellow]Enter custom array[/]"))
        {
            return PromptIntArray(title);
        }

        // Extract the example value (remove the prefix)
        var exampleIndex = int.Parse(selected.Split(':')[0].Replace("[green]Example ", "").Replace("[/]", "")) - 1;
        return examples[exampleIndex];
    }

    public int[] PromptIntTwoRankArrayOrChoice(string title, int[][] examples)
    {
        var choices = new List<string> { "[yellow]Enter custom array[/]" };
        choices.AddRange(examples.Select((e, i) => $"[green]Example {i + 1}:[/] [[{string.Join(", ", e)}]]"));

        var selected = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"[bold]{title}[/]")
                .AddChoices(choices)
        );

        if (selected.StartsWith("[yellow]Enter custom array[/]"))
        {
            return PromptIntArray(title);
        }

        var exampleIndex = int.Parse(selected.Split(':')[0].Replace("[green]Example ", "").Replace("[/]", "")) - 1;
        return examples[exampleIndex];
    }

    public int[] PromptIntArrayOrChoice(string title, int[] examples)
    {
        var choices = new List<string> { "[yellow]Enter custom array[/]" };
        choices.AddRange(examples.Select((e, i) => $"[green]Example {i + 1}:[/] {e}"));

        var selected = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"[bold]{title}[/]")
                .AddChoices(choices)
        );

        if (selected.StartsWith("[yellow]Enter custom array[/]"))
        {
            return PromptIntArray(title);
        }

        var exampleIndex = int.Parse(selected.Split(':')[0].Replace("[green]Example ", "").Replace("[/]", "")) - 1;
        return [examples[exampleIndex]];
    }

    public int PromptIntOrChoice(string title, int example)
    {
        var choices = new List<string>
        {
            "[yellow]Enter custom value[/]",
            $"[green]Example:[/] {example}"
        };

        var selected = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"[bold]{title}[/]")
                .AddChoices(choices)
        );

        if (selected.StartsWith("[yellow]Enter custom value[/]"))
        {
            return AnsiConsole.Ask<int>("[bold]Enter your value:[/]");
        }

        return example;
    }
}
