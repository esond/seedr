using System.Reactive;
using System.Reactive.Concurrency;
using ReactiveUI;
using Seedr.Controller.Client;
using Seedr.Controller.Interface.Contracts;
using Splat;

namespace Seedr.Monitor.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly ControllerClientFactory _clientFactory;

    private SeederSettingsViewModel _seederSettings = new(new SeederSettings());

    public MainViewModel(string title,
        IScheduler? mainThreadScheduler = null,
        IScheduler? taskPoolScheduler = null,
        IScreen? hostScreen = null,
        ControllerClientFactory? clientFactory = null)
        : base(title, mainThreadScheduler, taskPoolScheduler, hostScreen)
    {
        _clientFactory = clientFactory ?? Locator.Current.GetService<ControllerClientFactory>()!;
    }

    public SeederSettingsViewModel SeederSettings
    {
        get => _seederSettings;
        set => this.RaiseAndSetIfChanged(ref _seederSettings, value);
    }

    public ReactiveCommand<Unit, Unit> DecreaseFanSpeedCommand => ReactiveCommand.CreateFromTask(async () =>
    {
        var client = _clientFactory.Create();
        var settings = await client.SetFanSpeed(SeederSettings.FanSpeed - 100);

        SeederSettings = new SeederSettingsViewModel(settings);

        return Unit.Default;
    });

    public ReactiveCommand<Unit, Unit> IncreaseFanSpeedCommand => ReactiveCommand.CreateFromTask(async () =>
    {
        var client = _clientFactory.Create();
        var settings = await client.SetFanSpeed(SeederSettings.FanSpeed + 100);

        SeederSettings = new SeederSettingsViewModel(settings);

        return Unit.Default;
    });

    public ReactiveCommand<Unit, Unit> DecreaseSeedRateCommand => ReactiveCommand.CreateFromTask(async () =>
    {
        var client = _clientFactory.Create();
        var settings = await client.SetSeedRate(SeederSettings.SeedRate - 1);

        SeederSettings = new SeederSettingsViewModel(settings);

        return Unit.Default;
    });

    public ReactiveCommand<Unit, Unit> IncreaseSeedRateCommand => ReactiveCommand.CreateFromTask(async () =>
    {
        var client = _clientFactory.Create();
        var settings = await client.SetSeedRate(SeederSettings.SeedRate + 1);

        SeederSettings = new SeederSettingsViewModel(settings);

        return Unit.Default;
    });
}
