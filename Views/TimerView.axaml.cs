using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using PomodoroTimer.Services;
using PomodoroTimer.ViewModels;

namespace PomodoroTimer.Views;

public partial class TimerView : UserControl
{
    public TimerView()
    {
        InitializeComponent();
        this.DataContext = new TimerViewModel(new SettingsService());
        Avalonia.Threading.Dispatcher.UIThread.Post(() =>
        {
            (DataContext as TimerViewModel)?.InitializeTimerCommand.Execute(null);
        });
    }
    
    private void ToggleButton_OnClick(object? sender, RoutedEventArgs e)
    {
        (DataContext as TimerViewModel)?.ToggleTimerCommand.Execute(null);
    }

    private void SettingsButton_OnClick(object? sender, RoutedEventArgs e)
    {
        (DataContext as TimerViewModel)?.ToggleSettingsViewCommand.Execute(null);
    }

    private void SkipButton_OnClick(object? sender, RoutedEventArgs e)
    {
        (DataContext as TimerViewModel)?.SkipCurrentModeCommand.Execute(null);
    }

    private void StopButton_OnClick(object? sender, RoutedEventArgs e)
    {
        (DataContext as TimerViewModel)?.ResetTimerCommand.Execute(null);
    }
}