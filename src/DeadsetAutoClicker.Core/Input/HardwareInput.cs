using System.Runtime.InteropServices;

namespace DeadsetAutoClicker.DeadsetAutoClicker.Core.Input;

[StructLayout(LayoutKind.Sequential)]
public struct HardwareInput
{
    public uint uMsg;
    public ushort wParamL;
    public ushort wParamH;
}
