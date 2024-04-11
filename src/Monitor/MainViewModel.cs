using System.Reactive;
using System.Reactive.Concurrency;
using ReactiveUI;

namespace Seedr.Monitor;

public class MainViewModel : ViewModelBase
{
    public MainViewModel(string title, IScheduler? mainThreadScheduler = null, IScheduler? taskPoolScheduler = null,
        IScreen? hostScreen = null)
        : base(title, mainThreadScheduler, taskPoolScheduler, hostScreen)
    {
    }

    public string TestText => "Hello world!";

    public ReactiveCommand<Unit, Unit> IncrementCountCommand { get; }

    public int Count { get; }

    public string CounterText => $"Clicked {Count} time(s)";
}
