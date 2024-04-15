using Seedr.Shared;
using Spectre.Console;

namespace Seedr.Control;
public static class ConsoleView
{
    public static async Task InitializeAsync()
    {
        var settingsTable = new Table();

        settingsTable.AddColumn("Name");
        settingsTable.AddColumn("Value");

        var settings = new SeederSettings { FanSpeed = 4000, SeedRate = 156.92 };

        settingsTable.AddRow(nameof(settings.FanSpeed), $"{settings.FanSpeed:N} RPM");
        settingsTable.AddRow(nameof(settings.SeedRate), $"{settings.SeedRate:N} kg/ha");

        var settingsPanel = new Panel(settingsTable)
        {
            Header = new PanelHeader("Current settings"),
        };


        await AnsiConsole.Live(settingsPanel)
            .Start(_ => Task.CompletedTask);

        AnsiConsole.Status()
            .Start("Running...", ctx =>
            {
                ctx.Spinner(Spinner.Known.Dots);

                Thread.Sleep(Timeout.Infinite);
            });
    }
}
