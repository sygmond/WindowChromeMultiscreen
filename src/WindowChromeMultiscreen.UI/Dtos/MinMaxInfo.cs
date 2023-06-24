namespace WindowChromeMultiscreen.UI.Dtos;

[StructLayout(LayoutKind.Sequential)]
public struct MinMaxInfo
{
    public Point PointReserved;
    public Point PointMaxSize;
    public Point PointMaxPosition;
    public Point PointMinTrackSize;
    public Point PointMaxTrackSize;
}