using Hexagrams.Extensions.Configuration;
using ReactiveUI;
using Seedr.Controller.Client;
using Seedr.Monitor.ViewModels;
using Splat;

namespace Seedr.Monitor;

public static class AppBootstrapper
{
    /// <summary>
    /// Registers everything with the Splat service locator.
    /// </summary>
    public static MauiAppBuilder UseAppBootstrapper(this MauiAppBuilder builder)
    {
        var router = new RoutingState();
        var screen = new AppBootstrapScreen(router);

        Locator.CurrentMutable.RegisterConstant(screen, typeof(IScreen));

        Locator.CurrentMutable.Register(() => new MainPage(), typeof(IViewFor<MainViewModel>));
        Locator.CurrentMutable.Register(() => new ControllerClientFactory(new ControllerClientOptions
        {
            ControllerUrl = builder.Configuration.Require(ConfigConstants.ControllerUrl)
        }));

        router
            .NavigateAndReset
            .Execute(new MainViewModel("Seedr"))
            .Subscribe();

        return builder;
    }

    /// <summary>
    /// Creates the first main page used within the application.
    /// </summary>
    /// <returns>The page generated.</returns>
    public static Page CreateMainPage()
    {
        // NB: This returns the opening page that the platform-specific
        // boilerplate code will look for. It will know to find us because
        // we've registered our AppBootstrapScreen.
        return new ReactiveUI.Maui.RoutedViewHost();
    }

    /// <summary>
    /// The app bootstrap screen is the central location for the RoutingState used for routing between views.
    /// </summary>
    private class AppBootstrapScreen : ReactiveObject, IScreen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppBootstrapScreen"/> class.
        /// </summary>
        public AppBootstrapScreen(RoutingState router)
        {
            Router = router;
        }

        /// <summary>
        /// Gets or sets the router which is used to navigate between views.
        /// </summary>
        public RoutingState Router { get; protected set; }
    }
}
