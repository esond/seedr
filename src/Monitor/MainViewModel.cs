using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Seedr.Monitor;

public class MainViewModel : ReactiveObject, IActivatableViewModel
{

    public MainViewModel()
    {
        IncrementCountCommand = ReactiveCommand.Create(IncrementCount);

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
            // Just log the ViewModel's activation
            // https://github.com/kentcb/YouIandReactiveUI/blob/master/ViewModels/Samples/Chapter%2018/Sample%2004/ChildViewModel.cs
            Console.WriteLine(
                $"[vm {Environment.CurrentManagedThreadId}]: " +
                "ViewModel activated");

            // Just log the ViewModel's deactivation
            // https://github.com/kentcb/YouIandReactiveUI/blob/master/ViewModels/Samples/Chapter%2018/Sample%2004/ChildViewModel.cs
            Disposable
                .Create(
                    () =>
                        Console.WriteLine(
                            $"[vm {Environment.CurrentManagedThreadId}]: " +
                            "ViewModel deactivated"))
                .DisposeWith(disposables);
        });
    }

    public ReactiveCommand<Unit, Unit> IncrementCountCommand { get; }

    public void IncrementCount()
    {
        Count++;
    }

    [Reactive]
    public int Count { get; set; }

    [ObservableAsProperty]
    public string? CounterText { get; }

    public ViewModelActivator Activator { get; } = new();
}
