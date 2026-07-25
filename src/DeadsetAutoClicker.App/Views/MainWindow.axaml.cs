using System;
using System.ComponentModel;

using Avalonia.Controls;

using DeadsetAutoClicker.App.ViewModels;
using DeadsetAutoClicker.DeadsetAutoClicker.Core.Interop;

namespace DeadsetAutoClicker.App.Views;

public partial class MainWindow : Window
{
    private const int CTRL_F12_HOTKEY = 1;
    private const int ALT_F12_HOTKEY = 2;

    private const uint MOD_ALT = 0x0001;
    private const uint MOD_CONTROL = 0x0002;
    private const uint VK_F12 = 0x7B;

    private const uint WM_HOTKEY = 0x0312;

    private IntPtr _WindowHandle;
    private HomeViewModel? _Home;

    public MainWindow()
    {
        InitializeComponent();
    }

    private HomeViewModel? Home
    {
        get
        {
            if (_Home is null && DataContext is MainWindowViewModel mainWindowViewModel)
            {
                _Home = mainWindowViewModel.Content as HomeViewModel;

                if (_Home is not null)
                {
                    _Home.PropertyChanged += Home_PropertyChanged;
                }
            }

            return _Home;
        }
    }

    protected override void OnOpened(EventArgs e)
    {
        base.OnOpened(e);

        // Touch the view model so we start listening for start/stop.
        _ = Home;

        if (!OperatingSystem.IsWindows())
        {
            return;
        }

        _WindowHandle = TryGetPlatformHandle()?.Handle ?? IntPtr.Zero;

        if (_WindowHandle == IntPtr.Zero)
        {
            return;
        }

        Win32Properties.AddWndProcHookCallback(this, WndProcHook);
        NativeMethods.RegisterHotKey(_WindowHandle, CTRL_F12_HOTKEY, MOD_CONTROL, VK_F12);
        NativeMethods.RegisterHotKey(_WindowHandle, ALT_F12_HOTKEY, MOD_ALT, VK_F12);
    }

    protected override void OnClosing(WindowClosingEventArgs e)
    {
        if (OperatingSystem.IsWindows() && _WindowHandle != IntPtr.Zero)
        {
            NativeMethods.UnregisterHotKey(_WindowHandle, CTRL_F12_HOTKEY);
            NativeMethods.UnregisterHotKey(_WindowHandle, ALT_F12_HOTKEY);
            Win32Properties.RemoveWndProcHookCallback(this, WndProcHook);
            _WindowHandle = IntPtr.Zero;
        }

        if (_Home is not null)
        {
            _Home.PropertyChanged -= Home_PropertyChanged;
            _Home.StopCommand.Execute(null);
        }

        base.OnClosing(e);
    }

    private IntPtr WndProcHook(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        if (msg == WM_HOTKEY)
        {
            switch (wParam.ToInt32())
            {
                case CTRL_F12_HOTKEY:
                    Home?.StartCommand.Execute(null);
                    handled = true;
                    break;

                case ALT_F12_HOTKEY:
                    Home?.StopCommand.Execute(null);
                    handled = true;
                    break;
            }
        }

        return IntPtr.Zero;
    }

    private void Home_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(HomeViewModel.IsStarted) || _Home is null)
        {
            return;
        }

        // Get out of the way while clicking, come back when it stops - same as the WinForms version.
        WindowState = _Home.IsStarted ? WindowState.Minimized : WindowState.Normal;
    }
}
