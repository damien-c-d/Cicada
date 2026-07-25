using System.Diagnostics;
using System.Runtime.InteropServices;
using Cicada.Core.Input;

namespace Cicada.Core.Interop;
public static class NativeMethods
{
    [DllImport("user32.dll")]
    public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

    [DllImport("user32.dll")]
    public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern uint SendInput(uint nInputs, Input.Input[] pInputs, int cbSize);

    [DllImport("user32.dll")]
    public static extern IntPtr GetMessageExtraInfo();

    private static readonly Input.Input[] _LeftClickInputs = { Input.Input.MouseLeftDown, Input.Input.MouseLeftUp };
    private static readonly Input.Input[] _RightClickInputs = { Input.Input.MouseRightDown, Input.Input.MouseRightUp };
    private static readonly Input.Input[] _MiddleClickInputs = { Input.Input.MouseMiddleDown, Input.Input.MouseMiddleUp };

    public static Input.Input[] GetInputs(ClickType clickType)
    {
        return clickType switch
        {
            ClickType.Left => _LeftClickInputs,
            ClickType.Right => _RightClickInputs,
            ClickType.Middle => _MiddleClickInputs,
            _ => throw new UnreachableException(),
        };
    }
}
