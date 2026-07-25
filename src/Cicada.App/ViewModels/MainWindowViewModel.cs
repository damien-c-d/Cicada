using CommunityToolkit.Mvvm.ComponentModel;

using Cicada.App.Services;

namespace Cicada.App.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] public partial ViewModelBase Content { get; set; }

    public string[] Themes { get; } = ThemeService.Options;

    [ObservableProperty] public partial string SelectedTheme { get; set; }

    public MainWindowViewModel()
    {
        Content = new HomeViewModel();
        SelectedTheme = ThemeService.Load();
    }

    partial void OnSelectedThemeChanged(string value)
    {
        ThemeService.Apply(value);
        ThemeService.Save(value);
    }
}
