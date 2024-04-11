using System.Reactive.Disposables;
using ReactiveUI;

namespace Seedr.Monitor;

public partial class MainPage : ContentPageBase<MainViewModel>
{
    public MainPage()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel, x => x.CounterText, x => x.CounterButton.Text, x => x).DisposeWith(disposables);
            this.OneWayBind(ViewModel, x => x.TestText, x => x.TestLabel.Text, x => x).DisposeWith(disposables);
        });
    }
}

