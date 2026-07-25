using System;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Cicada.App.Services;
using Cicada.Core.Input;

namespace Cicada.App.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    private readonly AutoClickingService _AutoClickingService = new();

    [ObservableProperty, NotifyPropertyChangedFor(nameof(ClickType))]
    public partial bool IsLeftClick { get; set; }

    [ObservableProperty, NotifyPropertyChangedFor(nameof(ClickType))]
    public partial bool IsRightClick { get; set; }

    [ObservableProperty, NotifyPropertyChangedFor(nameof(ClickType))]
    public partial bool IsMiddleClick { get; set; }

    [ObservableProperty, NotifyPropertyChangedFor(nameof(ClickAmount))] public partial bool ClickAmountTypeSingle { get; set; }
    [ObservableProperty, NotifyPropertyChangedFor(nameof(ClickAmount))] public partial bool ClickAmountTypeDouble { get; set; }
    [ObservableProperty, NotifyPropertyChangedFor(nameof(ClickAmount))] public partial bool ClickAmountTypeTriple { get; set; }

    [ObservableProperty, NotifyPropertyChangedFor(nameof(RepeatSetCount))] public partial bool RepeatInfinitely { get; set; }

    [ObservableProperty] public partial int RepeatCount { get; set; }

    /// <summary>Delay before each click, in seconds.</summary>
    [ObservableProperty] public partial int Delay { get; set; }

    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(StartCommand)), NotifyCanExecuteChangedFor(nameof(StopCommand))]
    public partial bool IsStarted { get; set; }

    /// <summary>Inverse of <see cref="RepeatInfinitely"/>, for the "Set Count" radio button.</summary>
    public bool RepeatSetCount
    {
        get => !RepeatInfinitely;
        set => RepeatInfinitely = !value;
    }

    public ClickType ClickType
    {
        get
        {
            if (IsRightClick) return ClickType.Right;
            if (IsMiddleClick) return ClickType.Middle;

            return ClickType.Left;
        }
        set
        {
            IsLeftClick = value == ClickType.Left;
            IsRightClick = value == ClickType.Right;
            IsMiddleClick = value == ClickType.Middle;
        }
    }

    public ClickAmount ClickAmount
    {
        get
        {
            if (ClickAmountTypeDouble) return ClickAmount.Double;
            if (ClickAmountTypeTriple) return ClickAmount.Triple;

            return ClickAmount.Single; // Default to Single if none are selected
        }
        set
        {
            ClickAmountTypeSingle = value == ClickAmount.Single;
            ClickAmountTypeDouble = value == ClickAmount.Double;
            ClickAmountTypeTriple = value == ClickAmount.Triple;
        }
    }

    public HomeViewModel()
    {
        ClickType = ClickType.Left;
        ClickAmount = ClickAmount.Single;
        RepeatCount = 1;
        RepeatInfinitely = true;
        Delay = 1;
    }

    [RelayCommand(CanExecute = nameof(CanStart))]
    private async Task StartAsync()
    {
        IsStarted = true;

        try
        {
            await _AutoClickingService.StartAsync(ClickType, ClickAmount, TimeSpan.FromSeconds(Delay), RepeatInfinitely, RepeatCount);
        }
        finally
        {
            IsStarted = false;
        }
    }

    private bool CanStart() => !IsStarted;

    [RelayCommand(CanExecute = nameof(CanStop))]
    private void Stop() => _AutoClickingService.Stop();

    private bool CanStop() => IsStarted;
}
