using System;
using PomodoroTimer.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PomodoroTimer.Services;

namespace PomodoroTimer.ViewModels;

public partial class SettingsViewModel() : ViewModelBase
{
    private readonly SettingsService _settingsService;
    [ObservableProperty] private int _workDurationSetting;
    [ObservableProperty] private int _shortBreakDurationSetting;
    [ObservableProperty] private int _longBreakDurationSetting;
    [ObservableProperty] private int _pomodoroAmountSetting;
    [ObservableProperty] private bool _autoStartTimerSetting;
    
    public SettingsViewModel(SettingsService settingsService) : this()
    {
        _settingsService = settingsService;
        WorkDurationSetting = settingsService.AppSettings.WorkDuration;
        ShortBreakDurationSetting = settingsService.AppSettings.ShortBreakDuration;
        LongBreakDurationSetting = settingsService.AppSettings.LongBreakDuration;
        PomodoroAmountSetting = settingsService.AppSettings.PomodorosUntilLongBreak;
        AutoStartTimerSetting = settingsService.AppSettings.AutomaticallyStartNextTimer;
    }


    [RelayCommand]
    private void Save()
    {
        _settingsService.AppSettings.WorkDuration = WorkDurationSetting;
        _settingsService.AppSettings.ShortBreakDuration = ShortBreakDurationSetting;
        _settingsService.AppSettings.LongBreakDuration = LongBreakDurationSetting;
        _settingsService.AppSettings.PomodorosUntilLongBreak = PomodoroAmountSetting;
        _settingsService.AppSettings.AutomaticallyStartNextTimer = AutoStartTimerSetting;
        _settingsService.Save();
    }
}
