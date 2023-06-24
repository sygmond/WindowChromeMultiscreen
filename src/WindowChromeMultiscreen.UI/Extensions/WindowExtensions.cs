namespace WindowChromeMultiscreen.UI.Extensions;

public static class WindowExtensions
{
    public static Size GetCurrentMonitorSize(this Window window)
    {
        var hwnd = new WindowInteropHelper(window).EnsureHandle();
        var screenSize = new Size();

        if (NativeMethods.TryGetCurrentMonitorInfo(hwnd, out NativeMonitorInfo currentMonitorInfo))
        {
            screenSize.Width = Math.Abs(currentMonitorInfo.Work.Right - currentMonitorInfo.Work.Left);
            screenSize.Height = Math.Abs(currentMonitorInfo.Work.Bottom - currentMonitorInfo.Work.Top);
        }

        return screenSize;
    }
}