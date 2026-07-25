using CommunityToolkit.Mvvm.ComponentModel;

namespace DeadsetAutoClicker.App.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] public partial ViewModelBase Content { get; set; }

    public MainWindowViewModel()
    {
        Content = new HomeViewModel();
    }


}
