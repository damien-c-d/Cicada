using System;
using System.IO;

using Avalonia;
using Avalonia.Styling;

namespace Cicada.App.Services;

/// <summary>Applies and remembers the user's light/dark/system theme choice.</summary>
public static class ThemeService
{
    public const string System = "System";
    public const string Light = "Light";
    public const string Dark = "Dark";

    public static string[] Options { get; } = [System, Light, Dark];

    // ponytail: one line of text in AppData is enough for a single setting - no settings framework.
    private static string SettingsFile => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "Cicada",
        "theme.txt");

    public static string Load()
    {
        try
        {
            var saved = File.ReadAllText(SettingsFile).Trim();

            return Array.IndexOf(Options, saved) >= 0 ? saved : System;
        }
        catch
        {
            return System;
        }
    }

    public static void Save(string theme)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(SettingsFile)!);
            File.WriteAllText(SettingsFile, theme);
        }
        catch
        {
            // Not being able to remember the theme isn't worth failing over.
        }
    }

    public static void Apply(string theme)
    {
        if (Application.Current is null)
        {
            return;
        }

        Application.Current.RequestedThemeVariant = theme switch
        {
            Light => ThemeVariant.Light,
            Dark => ThemeVariant.Dark,
            _ => ThemeVariant.Default, // Follows the Windows app theme.
        };
    }
}
