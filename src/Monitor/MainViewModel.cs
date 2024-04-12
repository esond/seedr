using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Seedr.Monitor;

public class MainViewModel : ReactiveObject, IActivatableViewModel
{
    public MainViewModel(ILogger<MainViewModel> logger)
    {
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

    // TODO: commands should send an message to the controller, which will report back with current settings that we display.
    public ReactiveCommand<Unit, int> IncrementCountCommand => ReactiveCommand.Create(() => Count++);

    public ReactiveCommand<Unit, int> DecreaseSpeedCommand => ReactiveCommand.Create(() => Speed--);
    public ReactiveCommand<Unit, int> IncreaseSpeedCommand => ReactiveCommand.Create(() => Speed++);
    public ReactiveCommand<Unit, int> DecreaseSeedRateCommand => ReactiveCommand.Create(() => SeedRate--);
    public ReactiveCommand<Unit, int> IncreaseSeedRateCommand => ReactiveCommand.Create(() => SeedRate++);

    [Reactive]
    public int Count { get; set; }

    [Reactive]
    public int Speed { get; set; }

    [Reactive]
    public int SeedRate { get; set; }

    [ObservableAsProperty]
    public string? CounterText { get; }
}
