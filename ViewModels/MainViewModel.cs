using System.Timers;
using Avalonia.Controls;
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
    private readonly CalendarViewModel _calendarViewModel;
    
    public MainViewModel()
    {
        _settingsViewModel = new SettingsViewModel(_settingsService, this);
        _timerViewModel = new TimerViewModel(_settingsService);
        _calendarViewModel = new CalendarViewModel();
        CurrentView = _timerViewModel;
    }
    
    [RelayCommand]
    public void ToggleSettingsView()
    {
        if (CurrentView is SettingsViewModel)
        {
            CurrentView = _timerViewModel;
        }
        else
        {
            _settingsViewModel.LoadSettings();
            CurrentView = _settingsViewModel;
        }
    }
    
    [RelayCommand]
    public void ToggleCalendarView()
    {
        if (CurrentView is CalendarViewModel)
        {
            CurrentView = _timerViewModel;
        }
        else
        {
            CurrentView = _calendarViewModel;
        }
    }
    
}