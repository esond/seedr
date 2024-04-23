using System.Reactive.Concurrency;
using ReactiveUI;
using Splat;

namespace Seedr.Monitor;

public abstract class ViewModelBase : ReactiveObject, IRoutableViewModel, IActivatableViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
    /// </summary>
    /// <param name="title">The title of the view model for routing purposes.</param>
    /// <param name="mainThreadScheduler">The scheduler to use to schedule operations on the main thread.</param>
    /// <param name="taskPoolScheduler">The scheduler to use to schedule operations on the task pool.</param>
    /// <param name="hostScreen">The screen used for routing purposes.</param>
    protected ViewModelBase(string title, IScheduler? mainThreadScheduler = null, IScheduler? taskPoolScheduler = null, IScreen? hostScreen = null)
    {
        UrlPathSegment = title;
        HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>()!;

        // Set the schedulers like this so that we can inject the test scheduler later on when doing VM unit tests
        MainThreadScheduler = mainThreadScheduler ?? RxApp.MainThreadScheduler;
        TaskPoolScheduler = taskPoolScheduler ?? RxApp.TaskpoolScheduler;
    }

    /// <summary>
    /// Gets the current page path.
    /// </summary>
    public string UrlPathSegment { get; }

    /// <summary>
    /// Gets the screen used for routing operations.
    /// </summary>
    public IScreen HostScreen { get; }

    /// <summary>
    /// Gets the activator which contains context information for use in activation of the view model.
    /// </summary>
    public ViewModelActivator Activator { get; } = new();

    /// <summary>
    /// Gets the scheduler for scheduling operations on the main thread.
    /// </summary>
    protected IScheduler MainThreadScheduler { get; }

    /// <summary>
    /// Gets the scheduler for scheduling operations on the task pool.
    /// </summary>
    protected IScheduler TaskPoolScheduler { get; }
}
