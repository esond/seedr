using ReactiveUI;

namespace Seedr.Monitor;

public abstract class ViewModelBase : ReactiveObject, IActivatableViewModel
{
    public ViewModelActivator Activator { get; } = new();
}
