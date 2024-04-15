using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Seedr.Monitor.Infrastructure;
using Seedr.Shared.Contracts;

namespace Seedr.Monitor;

public class MainViewModel(IControllerClient controllerClient) : ViewModelBase
{
    [Reactive]
    public SeederSettingsModel SeederSettings { get; set; } = new(new SeederSettings());

    public ReactiveCommand<Unit, Unit> DecreaseFanSpeedCommand => ReactiveCommand.CreateFromTask(async () =>
    {
        var settings = await controllerClient.SetFanSpeed(SeederSettings.FanSpeed - 100);

        SeederSettings = new SeederSettingsModel(settings);

        return Unit.Default;
    });

    public ReactiveCommand<Unit, Unit> IncreaseFanSpeedCommand => ReactiveCommand.CreateFromTask(async () =>
    {
        var settings = await controllerClient.SetFanSpeed(SeederSettings.FanSpeed + 100);

        SeederSettings = new SeederSettingsModel(settings);

        return Unit.Default;
    });

    public ReactiveCommand<Unit, Unit> DecreaseSeedRateCommand => ReactiveCommand.CreateFromTask(async () =>
    {
        var settings = await controllerClient.SetSeedRate(SeederSettings.SeedRate - 1);

        SeederSettings = new SeederSettingsModel(settings);

        return Unit.Default;
    });

    public ReactiveCommand<Unit, Unit> IncreaseSeedRateCommand => ReactiveCommand.CreateFromTask(async () =>
    {
        var settings = await controllerClient.SetSeedRate(SeederSettings.SeedRate + 1);

        SeederSettings = new SeederSettingsModel(settings);

        return Unit.Default;
    });
}

public class SeederSettingsModel(SeederSettings settings) : ReactiveObject
{
    [Reactive]
    public int FanSpeed { get; set; } = settings.FanSpeed;

    [Reactive]
    public double SeedRate { get; set; } = settings.SeedRate;

    public SeederSettings ToContract()
    {
        return new SeederSettings { FanSpeed = FanSpeed, SeedRate = SeedRate };
    }
}
