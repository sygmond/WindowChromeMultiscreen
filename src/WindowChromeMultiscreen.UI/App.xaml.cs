namespace WindowChromeMultiscreen.UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost _host;
    private IServiceProvider Services => _host.Services;

    public App()
    {
        _host = Host
            .CreateDefaultBuilder()
            .ConfigureServices(ConfigureServices)
            .Build();
    }

    private async void AppStartup(object sender, StartupEventArgs e)
    {
        await _host.StartAsync();
        GetService<IMainWindow>().Show();
    }

    private async void AppExit(object sender, ExitEventArgs e)
    {
        using (_host)
            await _host.StopAsync(TimeSpan.FromSeconds(5));
    }

    private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddTransient<IMainWindow, MainWindow>();
        services.AddSingleton<IAppState, AppState>();
    }

    private T GetService<T>() => Services.GetRequiredService<T>();
}