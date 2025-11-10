using Playground;
using Spectre.Console;

var runners = RunnerFactory.Create().GetRunners();

if (runners is null || runners.Count == 0)
{
    Console.WriteLine("No runners found.");
    return;
}

var table = new Table();
table.AddColumn("ID");
table.AddColumn("Runner");

int id = 1;
foreach (var runner in runners)
{
    table.AddRow(id.ToString(), runner.Name);
    id++;
}

if (table is null)
{
    return;
}

while(true)
{
    AnsiConsole.Write(table.ToString()!);

    var choices = runners.Select(r => r.Name).ToList();

    var selectedRunnerName = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Please select a runner:")
            .AddChoices(choices)
    );

    var selectedRunner = runners.First(r => r.Name == selectedRunnerName);

    await selectedRunner.Run();
}