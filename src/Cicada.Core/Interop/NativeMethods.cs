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

    /// <summary>Windows 10 1809+. Draws the title bar in dark colours.</summary>
    public const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

    /// <summary>Windows 11 22H2+. Selects the DWM backdrop material.</summary>
    public const int DWMWA_SYSTEMBACKDROP_TYPE = 38;

    /// <summary>Mica - tinted by the desktop wallpaper.</summary>
    public const int DWMSBT_MAINWINDOW = 2;

    /// <summary>Acrylic - neutral grey, blurs whatever is behind the window.</summary>
    public const int DWMSBT_TRANSIENTWINDOW = 3;

    [DllImport("dwmapi.dll")]
    public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attribute, ref int value, int size);

    public static void SetWindowAttribute(IntPtr hwnd, int attribute, int value)
        => DwmSetWindowAttribute(hwnd, attribute, ref value, sizeof(int));

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
