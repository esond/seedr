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

        this.WhenAnyValue(vm => vm.Count)
            .Select(c => c switch
            {
                0 => "Click me",
                1 => "Clicked 1 time",
                _ => $"Clicked {c} times"
            })
            .ToPropertyEx(this, vm => vm.CounterText);

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
    public ReactiveCommand<Unit, int> IncrementCountCommand => ReactiveCommand.Create(() => Count++);

    public ReactiveCommand<Unit, int> DecreaseFanSpeedCommand => ReactiveCommand.Create(() => FanSpeed--);

    public ReactiveCommand<Unit, int> IncreaseFanSpeedCommand => ReactiveCommand.Create(() => FanSpeed++);

    public ReactiveCommand<Unit, double> DecreaseSeedRateCommand => ReactiveCommand.Create(() => SeedRate--);

    public ReactiveCommand<Unit, double> IncreaseSeedRateCommand => ReactiveCommand.Create(() => SeedRate++);

    public ReactiveCommand<Unit, SeederSettings> TestCommand =>
        ReactiveCommand.CreateFromTask(() => _controllerClient.SetFanSpeed(Random.Shared.Next(10)));

    [Reactive]
    public int Count { get; set; }

    [Reactive]
    public int FanSpeed { get; set; }

    [Reactive]
    public double SeedRate { get; set; }

    [ObservableAsProperty]
    public string? CounterText { get; }
}
