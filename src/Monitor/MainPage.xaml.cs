using ReactiveUI;
using ReactiveUI.Maui;

namespace Seedr.Monitor;

public partial class MainPage : ReactiveContentPage<MainViewModel>
{
    public MainPage(MainViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        this.WhenActivated(_ => { });
    }
}

