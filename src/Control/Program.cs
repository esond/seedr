using Seedr.Control;
using Spectre.Console;

var settingsTable = new Table();

settingsTable.AddColumn("Name");
settingsTable.AddColumn("Value");

var settings = new SeederSettings { Speed = 5.1, SeedRate = 156.92 };

settingsTable.AddRow(nameof(settings.Speed), $"{settings.Speed:N} km/h");
settingsTable.AddRow(nameof(settings.SeedRate), $"{settings.SeedRate:N} kg/ha");

var settingsPanel = new Panel(settingsTable)
{
    Header = new PanelHeader("Current settings"),
};


AnsiConsole.Live(settingsPanel)
    .Start(_ => Task.CompletedTask);

AnsiConsole.Status()
    .Start("Running...", ctx =>
    {
        ctx.Spinner(Spinner.Known.Dots);

        Thread.Sleep(Timeout.Infinite);
    });
