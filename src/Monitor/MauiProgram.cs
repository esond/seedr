using System.Reflection;
using Hexagrams.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Seedr.Monitor.Infrastructure;
using Seedr.Shared;

namespace Seedr.Monitor;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        BuildConfiguration(builder.Configuration);

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainViewModel>();

        builder.Services.AddSingleton(_ =>
            new ControllerClientFactory(new ControllerClientOptions
            {
                ControllerUrl = builder.Configuration.Require(ConfigConstants.ControllerUrl)
            }));

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    private static void BuildConfiguration(IConfigurationBuilder configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream("Seedr.Monitor.appsettings.json")!;

        configuration.AddJsonStream(stream);
    }
}
