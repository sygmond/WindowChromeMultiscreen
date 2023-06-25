namespace WindowChromeMultiscreen.UI.Core;

public class AppState : IAppState
{
    private readonly Tracker _tracker;

    public AppState()
    {
        _tracker = new Tracker();
        ConfigureWindow();
    }

    private void ConfigureWindow()
    {
        // This is the size of the window when the app starts for the first time
        const int defaultWindowWidth = 800;
        const int defaultWindowHeight = 450;

        _tracker.Configure<Window>()
            .Id(w =>
                {
                    var windowName = w.GetType().Name;
                    var totalScreensSize = SystemParameters.VirtualScreenWidth + "x" +
                                           SystemParameters.VirtualScreenHeight;

                    return windowName + "_" + totalScreensSize;
                },
                includeType: false)
            .Property(p => p.Width, defaultValue: defaultWindowWidth)
            .Property(p => p.Height, defaultValue: defaultWindowHeight)
            .Property(p => p.Top, defaultValue: 0)
            .Property(p => p.Left, defaultValue: 0)
            .Property(p => p.WindowState, defaultValue: WindowState.Normal)
            .PersistOn(nameof(Window.Closing))
            .WhenPersistingProperty((w, p) =>
            {
                p.Value = p.Property switch
                {
                    nameof(Window.Top) => Math.Min(Math.Max(w.Top, SystemParameters.VirtualScreenTop),
                        SystemParameters.VirtualScreenTop + SystemParameters.VirtualScreenHeight - w.ActualHeight),
                    nameof(Window.Left) => Math.Min(Math.Max(w.Left, SystemParameters.VirtualScreenLeft),
                        SystemParameters.VirtualScreenLeft + SystemParameters.VirtualScreenWidth - w.ActualWidth),
                    nameof(Window.Width) => Math.Min(Math.Max(w.Width, w.ActualWidth),
                        SystemParameters.VirtualScreenWidth),
                    nameof(Window.Height) => Math.Min(Math.Max(w.Height, w.ActualHeight),
                        SystemParameters.VirtualScreenHeight),
                    _ => p.Value
                };
            })
            .WhenAppliedState((p) =>
            {
                if (p.WindowState != WindowState.Minimized)
                    return;

                var monitorSize = p.GetCurrentMonitorSize();
                const double tolerance = 0.000002;

                if (Math.Abs(p.Width - monitorSize.Width) < tolerance &&
                    Math.Abs(p.Height - monitorSize.Height) < tolerance)
                    p.WindowState = WindowState.Maximized;
                else
                    p.WindowState = WindowState.Normal;
            })
            .StopTrackingOn(nameof(Window.Closing));
    }

    public void Track<T>(T item) => _tracker.Track(item);

    /// <summary>
    /// Save all tracked objects to its configured json file
    /// </summary>
    public void SaveAll() => _tracker.PersistAll();
}