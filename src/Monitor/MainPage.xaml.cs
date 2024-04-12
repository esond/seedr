using ReactiveUI;
using ReactiveUI.Maui;

namespace Seedr.Monitor;

public partial class MainPage : ReactiveContentPage<MainViewModel>
{
    public MainPage(MainViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            //this.OneWayBind(ViewModel, x => x.CounterText, x => x.CounterButton.Text, x => x).DisposeWith(disposables);
            //this.OneWayBind(ViewModel, x => x.TestText, x => x.TestLabel.Text, x => x).DisposeWith(disposables);

            //this.BindCommand(ViewModel, x => x.IncrementCountCommand, x => x.CounterButton.Command)
            //    .DisposeWith(disposables);
        });
    }
}

