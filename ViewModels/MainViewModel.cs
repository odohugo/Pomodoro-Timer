using System.Timers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PomodoroTimer.Services;

namespace PomodoroTimer.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private ViewModelBase _currentView;
    
    private readonly TimerViewModel _timerViewModel = new TimerViewModel(new SettingsService());
    private readonly SettingsViewModel _settingsViewModel = new SettingsViewModel(new SettingsService());

    public MainViewModel()
    {
        CurrentView = _timerViewModel;
    }

    [RelayCommand]
    public void ToggleView()
    {
        if (CurrentView is TimerViewModel)
        {
            CurrentView = _settingsViewModel;
        }
        else
        {
            CurrentView = _timerViewModel;
        }
    }
}