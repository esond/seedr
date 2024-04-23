using ReactiveUI;
using ReactiveUI.Maui;
using Seedr.Monitor.ViewModels;

namespace Seedr.Monitor;

public partial class MainPage : ReactiveContentPage<MainViewModel>
{
    public MainPage()
    {
        InitializeComponent();

        this.WhenActivated(_ => { });
    }
}
