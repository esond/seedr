using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Seedr.Monitor.Infrastructure;
using Seedr.Shared.Contracts;

namespace Seedr.Monitor;

public class MainViewModel : ReactiveObject, IActivatableViewModel
{
    private readonly IControllerClient _controllerClient;

    public MainViewModel(IControllerClient controllerClient, ILogger<MainViewModel> logger)
    {
        _controllerClient = controllerClient;

        //this.WhenAnyValue(vm => vm.SeederSettings.FanSpeed, vm => vm.SeederSettings.SeedRate);

        this.WhenActivated(disposables =>
        {
            logger.LogDebug("[vm {ThreadId}]: ViewModel activated", Environment.CurrentManagedThreadId);

            Disposable
                .Create(() =>
                    logger.LogDebug("[vm {ThreadId}]: ViewModel deactivated", Environment.CurrentManagedThreadId))
                .DisposeWith(disposables);
        });
    }
    public ViewModelActivator Activator { get; } = new();

    // TODO: commands should send a message to the controller, which will report back with current settings that we display.
    public ReactiveCommand<Unit, int> DecreaseFanSpeedCommand => ReactiveCommand.Create(() => SeederSettings.FanSpeed--);

    public ReactiveCommand<Unit, int> IncreaseFanSpeedCommand => ReactiveCommand.Create(() => SeederSettings.FanSpeed++);

    public ReactiveCommand<Unit, double> DecreaseSeedRateCommand => ReactiveCommand.Create(() => SeederSettings.SeedRate--);

    public ReactiveCommand<Unit, double> IncreaseSeedRateCommand => ReactiveCommand.Create(() => SeederSettings.SeedRate++);

    public ReactiveCommand<Unit, SeederSettings> TestCommand =>
        ReactiveCommand.CreateFromTask(() => _controllerClient.SetFanSpeed(Random.Shared.Next(10)));

    [Reactive]
    public SeederSettingsModel SeederSettings { get; set; } = new(new SeederSettings());
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
