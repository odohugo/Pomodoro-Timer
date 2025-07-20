using System.Timers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PomodoroTimer.Services;

namespace PomodoroTimer.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private ViewModelBase _currentView;
    
    private readonly SettingsService _settingsService = new SettingsService();
    private readonly TimerViewModel _timerViewModel;
    private readonly SettingsViewModel _settingsViewModel;

    public MainViewModel()
    {
        _settingsViewModel = new SettingsViewModel(_settingsService, this);
        _timerViewModel = new TimerViewModel(_settingsService);
        CurrentView = _timerViewModel;
    }

    [RelayCommand]
    public void ToggleView()
    {
        if (CurrentView is TimerViewModel)
        {
            _settingsViewModel.LoadSettings();
            CurrentView = _settingsViewModel;
        }
        else
        {
            CurrentView = _timerViewModel;
        }
    }
}