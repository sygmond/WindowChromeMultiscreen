// ReSharper disable InconsistentNaming

using System;
using System.Runtime.InteropServices;
using System.Windows;
using WindowChromeMultiscreen.Dtos;

namespace WindowChromeMultiscreen.Core;

public static class NativeMethods
{
    private const int WM_NCHITTEST = 0x0084;
    private const int WM_GETMINMAXINFO = 0x0024;
    private const uint MONITOR_DEFAULTTONEAREST = 0x00000002;

    /// <summary>
    /// The MonitorFromWindow function retrieves a handle to the display monitor
    /// that has the largest area of intersection with the bounding rectangle of
    /// a specified window.
    /// </summary>
    /// <param name="hwnd">A handle to the window of interest.</param>
    /// <param name="dwFlags">Determines the function's return value if the window
    /// does not intersect any display monitor.</param>
    /// <returns> If the window intersects one or more display monitor rectangles,
    /// the return value is an HMONITOR handle to the display monitor that has
    /// the largest area of intersection with the window. If the window does
    /// not intersect a display monitor, the return value depends on the value of dwFlags.
    /// Remarks: If the window is currently minimized, MonitorFromWindow uses
    /// the rectangle of the window before it was minimized.</returns>
    [DllImport("user32.dll")]
    private static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

    /// <summary>
    /// The GetMonitorInfo function retrieves information about a display monitor.
    /// </summary>
    /// <param name="hMonitor">A handle to the display monitor of interest.</param>
    /// <param name="lpmi">A pointer to a NativeMonitorInfo structure that
    /// receives information about the specified display monitor.</param>
    /// <returns>If the function succeeds, the return value is true.</returns>
    [DllImport("user32.dll")]
    private static extern bool GetMonitorInfo(IntPtr hMonitor, ref NativeMonitorInfo lpmi);

    /// <summary>
    /// Retrieves the dimensions of the bounding rectangle of the specified window.
    /// The dimensions are given in screen coordinates that are relative to the
    /// upper-left corner of the screen.
    /// </summary>
    /// <param name="hwnd">A handle to the window.</param>
    /// <param name="lpRect">A pointer to a NativeRectangle structure that receives the screen coordinates
    /// of the upper-left and lower-right corners of the window.</param>
    /// <returns>If the function succeeds, the return value is true.</returns>
    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool GetWindowRect(IntPtr hwnd, out Rect lpRect);

    public static IntPtr HookProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        switch (msg)
        {
            case WM_NCHITTEST:
                // Works around a Logitech mouse driver bug, code from
                // https://developercommunity.visualstudio.com/content/problem/167357/overflow-exception-in-windowchrome.html
                // This prevents a crash in WindowChromeWorker._HandleNCHitTest
                try
                {
                    lParam.ToInt32();
                }
                catch (OverflowException)
                {
                    handled = true;
                }

                break;

            case WM_GETMINMAXINFO:

                WmGetMinMaxInfo(hwnd, lParam);

                // Setting handled to false lets the handling of the window message fall through to default WPF mechanisms.
                // This way, the Min* attributes of the Window manage the minimum size
                // and the custom WmGetMinMaxInfo code manages the maximum size.
                handled = false;

                break;

            default:
                break;
        }

        return IntPtr.Zero;
    }

    /// <summary>
    /// The TryGetCurrentMonitorInfo function retrieves information about a display monitor.
    /// </summary>
    /// <param name="hwnd">A handle to the window of interest.</param>
    /// <param name="lpmi">Outputted NativeMonitorInfo structure that
    /// holds information about the current display monitor.</param>
    /// <returns>If the function succeeds, the return value is true.</returns>
    public static bool TryGetCurrentMonitorInfo(IntPtr hwnd, out NativeMonitorInfo lpmi)
    {
        var currentMonitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);
        lpmi = new NativeMonitorInfo() { Size = Marshal.SizeOf(typeof(NativeMonitorInfo)) };
        return currentMonitor != IntPtr.Zero && GetMonitorInfo(currentMonitor, ref lpmi);
    }

    private static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
    {
        var hasWindowRect = GetWindowRect(hwnd, out Rect lpRect);

        if (!hasWindowRect ||
            lpRect is { Left: < 0, Top: < 0, Right: < 0, Bottom: < 0 })
            return;

        // We need to tell the system what our size should be when maximized. Otherwise it will cover the whole screen,
        // including the task bar.
        if (Marshal.PtrToStructure(lParam, typeof(MinMaxInfo)) is not MinMaxInfo minMaxInfo)
            return;

        // Adjust the maximized size and position to fit the work area of the correct monitor
        if (TryGetCurrentMonitorInfo(hwnd, out NativeMonitorInfo currentMonitorInfo))
        {
            minMaxInfo.PointMaxPosition.X = Math.Abs(currentMonitorInfo.Work.Left - currentMonitorInfo.Monitor.Left);
            minMaxInfo.PointMaxPosition.Y = Math.Abs(currentMonitorInfo.Work.Top - currentMonitorInfo.Monitor.Top);
            minMaxInfo.PointMaxSize.X = Math.Abs(currentMonitorInfo.Work.Right - currentMonitorInfo.Work.Left);
            minMaxInfo.PointMaxSize.Y = Math.Abs(currentMonitorInfo.Work.Bottom - currentMonitorInfo.Work.Top);
            minMaxInfo.PointMaxTrackSize.X = minMaxInfo.PointMaxSize.X;
            minMaxInfo.PointMaxTrackSize.Y = minMaxInfo.PointMaxSize.Y;
        }

        Marshal.StructureToPtr(minMaxInfo, lParam, true);
    }
}