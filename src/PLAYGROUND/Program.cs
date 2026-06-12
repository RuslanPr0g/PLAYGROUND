using Playground;
using Spectre.Console;
using Spectre.Console.Cli;

var app = new CommandApp<RunCommand>();
app.Configure(config =>
{
    config.AddCommand<ListCommand>("list")
        .WithDescription("List all available runners");
    config.AddCommand<InteractiveCommand>("interactive")
        .WithDescription("Run in interactive mode with search");
});

return await app.RunAsync(args);

internal class RunCommand : AsyncCommand<RunCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "[name]")]
        public string? Name { get; set; }
    }

    protected override async Task<int> ExecuteAsync(CommandContext context, Settings settings, CancellationToken cancellationToken)
    {
        var runners = RunnerFactory.Create().GetRunners();
        var prompt = new SpectreConsolePrompt();

        if (string.IsNullOrEmpty(settings.Name))
        {
            AnsiConsole.Markup("[yellow]No runner specified. Use 'list' to see available runners or 'interactive' for selection.[/]");
            return 0;
        }

        var runner = runners.FirstOrDefault(r => r.Name.Equals(settings.Name, StringComparison.OrdinalIgnoreCase));
        if (runner == null)
        {
            AnsiConsole.Markup($"[red]Runner '{settings.Name}' not found.[/]");
            return 1;
        }

        AnsiConsole.Markup($"[green]Running: {runner.Name}[/]\n");
        await runner.Run(prompt);
        return 0;
    }
}

internal class EmptySettings : CommandSettings { }

internal class ListCommand : Command<EmptySettings>
{
    protected override int Execute(CommandContext context, EmptySettings settings, CancellationToken cancellationToken)
    {
        var runners = RunnerFactory.Create().GetRunners();

        var table = new Table();
        table.AddColumn(new TableColumn("[green]#[/]").Centered());
        table.AddColumn(new TableColumn("[cyan]Name[/]"));
        table.AddColumn(new TableColumn("[yellow]Description[/]"));

        int id = 1;
        foreach (var runner in runners)
        {
            table.AddRow(id.ToString(), $"[bold]{runner.Name}[/]", runner.Description);
            id++;
        }

        AnsiConsole.Write(table);
        return 0;
    }
}

internal class InteractiveCommand : AsyncCommand<EmptySettings>
{
    protected override async Task<int> ExecuteAsync(CommandContext context, EmptySettings settings, CancellationToken cancellationToken)
    {
        var runners = RunnerFactory.Create().GetRunners();
        var prompt = new SpectreConsolePrompt();

        while (true)
        {
            AnsiConsole.Clear();

            var table = new Table();
            table.AddColumn(new TableColumn("[green]#[/]").Centered());
            table.AddColumn(new TableColumn("[cyan]Name[/]"));
            table.AddColumn(new TableColumn("[yellow]Description[/]"));

            int id = 1;
            foreach (var runner in runners)
            {
                table.AddRow(id.ToString(), $"[bold]{runner.Name}[/]", runner.Description);
                id++;
            }

            AnsiConsole.Write(table);

            var choices = runners.Select(r => r.Name).ToList();
            choices.Add("[red]Exit[/]");

            var selectedRunnerName = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold]Select a runner to run:[/]")
                    .AddChoices(choices)
                    .UseConverter(s => s == "[red]Exit[/]" ? s : s)
            );

            if (selectedRunnerName == "[red]Exit[/]")
            {
                break;
            }

            var selectedRunner = runners.First(r => r.Name == selectedRunnerName);

            AnsiConsole.Markup($"\n[green]Running: {selectedRunner.Name}[/]\n");
            AnsiConsole.Markup($"[dim]{selectedRunner.Description}[/]\n\n");

            await selectedRunner.Run(prompt);

            AnsiConsole.Markup("\n[bold]Press any key to continue...[/]");
            Console.ReadKey();
        }

        return 0;
    }
}
