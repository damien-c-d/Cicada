using DeadsetAutoClicker.DeadsetAutoClicker.Core.Interop;

namespace DeadsetAutoClicker.DeadsetAutoClicker.Core.Input;

public struct Input
{
    public int type;
    public InputUnion u;

    public static Input MouseLeftDown => new() { type = (int)InputType.Mouse, u = new InputUnion { mi = new MouseInput { dwFlags = (uint)MouseEventF.LeftDown, dwExtraInfo = NativeMethods.GetMessageExtraInfo() } } };
    public static Input MouseLeftUp => new() { type = (int)InputType.Mouse, u = new InputUnion { mi = new MouseInput { dwFlags = (uint)MouseEventF.LeftUp, dwExtraInfo = NativeMethods.GetMessageExtraInfo() } } };
    public static Input MouseRightDown => new() { type = (int)InputType.Mouse, u = new InputUnion { mi = new MouseInput { dwFlags = (uint)MouseEventF.RightDown, dwExtraInfo = NativeMethods.GetMessageExtraInfo() } } };
    public static Input MouseRightUp => new() { type = (int)InputType.Mouse, u = new InputUnion { mi = new MouseInput { dwFlags = (uint)MouseEventF.RightUp, dwExtraInfo = NativeMethods.GetMessageExtraInfo() } } };
    public static Input MouseMiddleDown => new() { type = (int)InputType.Mouse, u = new InputUnion { mi = new MouseInput { dwFlags = (uint)MouseEventF.MiddleDown, dwExtraInfo = NativeMethods.GetMessageExtraInfo() } } };
    public static Input MouseMiddleUp => new() { type = (int)InputType.Mouse, u = new InputUnion { mi = new MouseInput { dwFlags = (uint)MouseEventF.MiddleUp, dwExtraInfo = NativeMethods.GetMessageExtraInfo() } } };

}
