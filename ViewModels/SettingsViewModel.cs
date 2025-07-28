using System;
using PomodoroTimer.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PomodoroTimer.Services;

namespace PomodoroTimer.ViewModels;

public partial class SettingsViewModel() : ViewModelBase
{
    private MainViewModel _mainViewModel;
    private TimerViewModel _timerViewModel;
    private readonly SettingsService _settingsService;
    [ObservableProperty] private int _workDurationSetting;
    [ObservableProperty] private int _shortBreakDurationSetting;
    [ObservableProperty] private int _longBreakDurationSetting;
    [ObservableProperty] private int _pomodoroAmountSetting;
    [ObservableProperty] private bool _autoStartTimerSetting;
    [ObservableProperty] private bool _gainPomodorosOnSkipSetting;
    
    public SettingsViewModel(SettingsService settingsService, MainViewModel mainViewModel) : this()
    {
        _mainViewModel  = mainViewModel;
        _settingsService = settingsService;
        LoadSettings();
    }

    public void LoadSettings()
    {
        WorkDurationSetting = _settingsService.AppSettings.WorkDuration;
        ShortBreakDurationSetting = _settingsService.AppSettings.ShortBreakDuration;
        LongBreakDurationSetting = _settingsService.AppSettings.LongBreakDuration;
        PomodoroAmountSetting = _settingsService.AppSettings.PomodorosUntilLongBreak;
        AutoStartTimerSetting = _settingsService.AppSettings.AutomaticallyStartNextTimer;
        GainPomodorosOnSkipSetting = _settingsService.AppSettings.GainPomodorosOnSkip;
    }


    [RelayCommand]
    private void Save()
    {
        _settingsService.AppSettings.WorkDuration = WorkDurationSetting;
        _settingsService.AppSettings.ShortBreakDuration = ShortBreakDurationSetting;
        _settingsService.AppSettings.LongBreakDuration = LongBreakDurationSetting;
        _settingsService.AppSettings.PomodorosUntilLongBreak = PomodoroAmountSetting;
        _settingsService.AppSettings.AutomaticallyStartNextTimer = AutoStartTimerSetting;
        _settingsService.AppSettings.GainPomodorosOnSkip = GainPomodorosOnSkipSetting;
        _settingsService.Save();
        _mainViewModel.ToggleSettingsView();
    }

    [RelayCommand]
    private void ResetSettings()
    {
        _settingsService.AppSettings.WorkDuration = 25;
        _settingsService.AppSettings.ShortBreakDuration = 5;
        _settingsService.AppSettings.LongBreakDuration = 15;
        _settingsService.AppSettings.PomodorosUntilLongBreak = 4;
        _settingsService.AppSettings.AutomaticallyStartNextTimer = false;
        _settingsService.AppSettings.GainPomodorosOnSkip = false;
        _settingsService.Save();
        _mainViewModel.ToggleSettingsView();
    }
    
}
