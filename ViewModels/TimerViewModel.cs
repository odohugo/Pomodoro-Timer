using System.IO.Enumeration;
using System.Net.Mime;
using System.Runtime.InteropServices.Swift;
using System.Timers;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PomodoroTimer.Services;

namespace PomodoroTimer.ViewModels;

public partial class TimerViewModel : ViewModelBase
{
   private readonly SettingsService _settingsService;
   
   [ObservableProperty]
   [NotifyPropertyChangedFor(nameof(TimerDisplay))]
   private int _timerSeconds;
   [ObservableProperty]
   [NotifyPropertyChangedFor(nameof(TimerDisplay))]
   private int _timerMinutes;
   [ObservableProperty]
   [NotifyPropertyChangedFor(nameof(ToggleButtonText))]
   private bool _timerActive;

   [ObservableProperty]
   [NotifyPropertyChangedFor(nameof(PomodoroDisplay))]
   private int _pomodoros;
   
   [ObservableProperty]
   private bool _settingsViewActive;

   private Timer? _timer;
   public string TimerDisplay => $"{TimerMinutes:D2}:{TimerSeconds:D2}";
   public string PomodoroDisplay
   {
      get
      {
         if (Pomodoros == 1) return $"{Pomodoros} pomodoro";
         else return $"{Pomodoros} pomodoros";
      }
   }

   public string ToggleButtonText => TimerActive ? "Pause" : "Start";
   public enum Mode
   {
      Work,
      ShortBreak,
      LongBreak
   }
   
   [ObservableProperty]
   [NotifyPropertyChangedFor(nameof(WorkActive))]
   [NotifyPropertyChangedFor(nameof(ShortBreakActive))]
   [NotifyPropertyChangedFor(nameof(LongBreakActive))]
   private Mode _currentMode;

   public bool WorkActive => CurrentMode == Mode.Work;
   public bool ShortBreakActive => CurrentMode == Mode.ShortBreak;
   public bool LongBreakActive => CurrentMode == Mode.LongBreak;

   private readonly System.Media.SoundPlayer _player = new System.Media.SoundPlayer(@"notification_sound.wav");
   public TimerViewModel(SettingsService settingsService)
   {
     _settingsService =  settingsService;
     TimerMinutes = _settingsService.AppSettings.WorkDuration;
   }
   
   [RelayCommand]
   private void InitializeTimer()
   {
      _timer = new Timer(1000);
      _timer.Elapsed += UpdateTimerValue;
      _timer.AutoReset = true;
      CurrentMode = Mode.Work;
      TimerMinutes = _settingsService.AppSettings.WorkDuration;
   }

   private void UpdateTimerValue(object? sender, ElapsedEventArgs e)
   {
      if (TimerSeconds <= 1)
      {
         if (TimerMinutes <= 0)
         {
            HandleModeSwitch();
            HandleAlert();
            return;
         }
         TimerSeconds = 59;
         TimerMinutes -= 1;
      }
      else
      {
         TimerSeconds -= 1;
      }
   }
   
   [RelayCommand]
   private void ToggleTimer()
   {
      TimerActive = !TimerActive;
      if (TimerActive) _timer?.Start();
      else _timer?.Stop();
   }

   [RelayCommand]
   private void ToggleSettingsView()
   {
      SettingsViewActive = !SettingsViewActive;
   }

   private void HandleModeSwitch()
   {
      _timer?.Stop();
      TimerActive = false;
      switch (CurrentMode)
      {
         case Mode.Work:
            Pomodoros += 1;
            if (Pomodoros > 0 && Pomodoros % _settingsService.AppSettings.PomodorosUntilLongBreak == 0)
            {
              CurrentMode = Mode.LongBreak; 
              _settingsService.Load();
              TimerMinutes = _settingsService.AppSettings.LongBreakDuration;
            }
            else
            {
               CurrentMode = Mode.ShortBreak;
               _settingsService.Load();
               TimerMinutes = _settingsService.AppSettings.ShortBreakDuration;
            }
            break;
         case Mode.ShortBreak:
            CurrentMode = Mode.Work;
            _settingsService.Load();
            TimerMinutes = _settingsService.AppSettings.WorkDuration;
            break;
         case Mode.LongBreak:
            CurrentMode = Mode.Work;
            TimerMinutes = _settingsService.AppSettings.WorkDuration;
            break;
      }
      TimerSeconds = 0;
      if (_settingsService.AppSettings.AutomaticallyStartNextTimer) ToggleTimer();
   }

   private void HandleAlert()
   {
      App.WindowActivator.BringToFront();
      _player?.Play();
   }

   [RelayCommand]
   private void SkipCurrentMode()
   {
      HandleModeSwitch();
   }

   [RelayCommand]
   private void ResetTimer()
   {
      _timer?.Stop();
      TimerActive = false;
      switch (CurrentMode)
      {
         case Mode.Work:
            TimerMinutes = _settingsService.AppSettings.WorkDuration;
            break;
         case Mode.ShortBreak:
            TimerMinutes = _settingsService.AppSettings.ShortBreakDuration;
            break;
         case Mode.LongBreak:
            TimerMinutes = _settingsService.AppSettings.LongBreakDuration;
            break;
      }
      TimerSeconds = 0;
   }
}