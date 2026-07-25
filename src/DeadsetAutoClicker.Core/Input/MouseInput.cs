using System.Runtime.InteropServices;

namespace DeadsetAutoClicker.DeadsetAutoClicker.Core.Input;

[StructLayout(LayoutKind.Sequential)]
public struct MouseInput
{
    public int dx;
    public int dy;
    public uint mouseData;
    public uint dwFlags;
    public uint time;
    public nint dwExtraInfo;
}
