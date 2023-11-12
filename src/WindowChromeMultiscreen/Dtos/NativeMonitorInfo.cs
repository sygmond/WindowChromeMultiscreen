using System.Runtime.InteropServices;

namespace WindowChromeMultiscreen.Dtos;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public struct NativeMonitorInfo
{
    public int Size;
    public NativeRectangle Monitor;
    public NativeRectangle Work;
    public uint Flags;
}