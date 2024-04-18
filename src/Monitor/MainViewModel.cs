using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Seedr.Monitor.Infrastructure;
using Seedr.Shared.Contracts;

namespace Seedr.Monitor;

public class MainViewModel(ControllerClientFactory clientFactory) : ViewModelBase
{
    [Reactive]
    public SeederSettingsModel SeederSettings { get; set; } = new(new SeederSettings());

    public ReactiveCommand<Unit, Unit> DecreaseFanSpeedCommand => ReactiveCommand.CreateFromTask(async () =>
    {
        var client = clientFactory.Create();
        var settings = await client.SetFanSpeed(SeederSettings.FanSpeed - 100);

        SeederSettings = new SeederSettingsModel(settings);

        return Unit.Default;
    });

    public ReactiveCommand<Unit, Unit> IncreaseFanSpeedCommand => ReactiveCommand.CreateFromTask(async () =>
    {
        var client = clientFactory.Create();
        var settings = await client.SetFanSpeed(SeederSettings.FanSpeed + 100);

        SeederSettings = new SeederSettingsModel(settings);

        return Unit.Default;
    });

    public ReactiveCommand<Unit, Unit> DecreaseSeedRateCommand => ReactiveCommand.CreateFromTask(async () =>
    {
        var client = clientFactory.Create();
        var settings = await client.SetSeedRate(SeederSettings.SeedRate - 1);

        SeederSettings = new SeederSettingsModel(settings);

        return Unit.Default;
    });

    public ReactiveCommand<Unit, Unit> IncreaseSeedRateCommand => ReactiveCommand.CreateFromTask(async () =>
    {
        var client = clientFactory.Create();
        var settings = await client.SetSeedRate(SeederSettings.SeedRate + 1);

        SeederSettings = new SeederSettingsModel(settings);

        return Unit.Default;
    });

    public class SeederSettingsModel(SeederSettings settings) : ReactiveObject
    {
        [Reactive]
        public int FanSpeed { get; set; } = settings.FanSpeed;

        [Reactive]
        public double SeedRate { get; set; } = settings.SeedRate;
    }
}
