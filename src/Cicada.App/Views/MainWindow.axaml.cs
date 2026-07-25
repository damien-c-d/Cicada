using System;
using System.ComponentModel;

using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;

using Cicada.App.ViewModels;
using Cicada.Core.Interop;

namespace Cicada.App.Views;

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
    private bool _ThemeChangedFired;

    /// <summary>DWMWA_SYSTEMBACKDROP_TYPE landed in Windows 11 22H2.</summary>
    private static bool BackdropSupported => OperatingSystem.IsWindowsVersionAtLeast(10, 0, 22621);

    public MainWindow()
    {
        InitializeComponent();

        if (BackdropSupported)
        {
            // Hand the whole window over to DWM and draw nothing behind our controls. Avalonia's own
            // Mica/AcrylicBlur levels only cover the client area and land on a different shade to the
            // title bar, which DWM paints - letting DWM do both is the only way they match.
            TransparencyLevelHint = [WindowTransparencyLevel.Transparent];
            Background = Brushes.Transparent;
        }
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

        if (BackdropSupported)
        {
            NativeMethods.SetWindowAttribute(_WindowHandle, NativeMethods.DWMWA_SYSTEMBACKDROP_TYPE, NativeMethods.DWMSBT_TRANSIENTWINDOW);
        }

        SyncTitleBarTheme();
        ActualThemeVariantChanged += (_, _) => { _ThemeChangedFired = true; SyncTitleBarTheme(); };

        if (Environment.GetEnvironmentVariable("CICADA_SELFCHECK") == "1")
        {
            RunThemeSelfCheck();
        }

        Win32Properties.AddWndProcHookCallback(this, WndProcHook);
        NativeMethods.RegisterHotKey(_WindowHandle, CTRL_F12_HOTKEY, MOD_CONTROL, VK_F12);
        NativeMethods.RegisterHotKey(_WindowHandle, ALT_F12_HOTKEY, MOD_ALT, VK_F12);
    }

    /// <summary>
    /// Checks that switching theme at runtime actually reaches the title bar. Set CICADA_SELFCHECK=1
    /// to run it; it writes pass/fail to %TEMP%\cicada-selfcheck.txt, and exits with the failure count.
    /// </summary>
    private void RunThemeSelfCheck()
    {
        var app = Avalonia.Application.Current!;
        var original = app.RequestedThemeVariant;
        var log = new System.Text.StringBuilder();
        var failures = 0;

        // Start from a known variant so each step below is a real change - setting the variant
        // it is already on raises nothing, which would make this check pass for the wrong reason.
        app.RequestedThemeVariant = ThemeVariant.Dark;

        foreach (var variant in new[] { ThemeVariant.Light, ThemeVariant.Dark })
        {
            _ThemeChangedFired = false;
            app.RequestedThemeVariant = variant;

            if (!_ThemeChangedFired)
            {
                log.AppendLine($"FAIL: switching to {variant} did not raise ActualThemeVariantChanged");
                failures++;
            }
            else if (ActualThemeVariant != variant)
            {
                log.AppendLine($"FAIL: requested {variant} but ActualThemeVariant is {ActualThemeVariant}");
                failures++;
            }
        }

        app.RequestedThemeVariant = original;
        log.AppendLine(failures == 0 ? "PASS: title bar theme sync wired up" : $"{failures} failure(s)");
        System.IO.File.WriteAllText(
            System.IO.Path.Combine(System.IO.Path.GetTempPath(), "cicada-selfcheck.txt"),
            log.ToString());
        // Exiting straight from OnOpened wedges the startup path, so let it unwind first.
        Avalonia.Threading.Dispatcher.UIThread.Post(() => Environment.Exit(failures));
    }

    /// <summary>
    /// DWM paints the title bar, so it doesn't follow the app's theme variant on its own -
    /// without this, picking Light while Windows is in dark mode leaves a dark title bar.
    /// </summary>
    private void SyncTitleBarTheme()
    {
        if (_WindowHandle == IntPtr.Zero)
        {
            return;
        }

        var isDark = ActualThemeVariant == ThemeVariant.Dark;
        NativeMethods.SetWindowAttribute(_WindowHandle, NativeMethods.DWMWA_USE_IMMERSIVE_DARK_MODE, isDark ? 1 : 0);
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
