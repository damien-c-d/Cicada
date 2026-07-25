using System.Runtime.InteropServices;

namespace DeadsetAutoClicker.DeadsetAutoClicker.Core.Input;

[StructLayout(LayoutKind.Sequential)]
public struct KeyboardInput
{
    public ushort wVk;
    public ushort wScan;
    public uint dwFlags;
    public uint time;
    public nint dwExtraInfo;
}
