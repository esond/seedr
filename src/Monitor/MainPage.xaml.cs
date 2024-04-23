using ReactiveUI;
using ReactiveUI.Maui;

namespace Seedr.Monitor;

public partial class MainPage : ReactiveContentPage<MainViewModel>
{
    public MainPage()
    {
        InitializeComponent();

        this.WhenActivated(_ => { });
    }
}
