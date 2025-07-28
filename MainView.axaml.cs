using Avalonia.Controls;
using Avalonia.Interactivity;
using PomodoroTimer.ViewModels;

namespace PomodoroTimer;

public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();
        
        this.DataContext = new MainViewModel();
    }

    private void SettingsButton_OnClick(object? sender, RoutedEventArgs e)
    {
        (DataContext as MainViewModel)?.ToggleSettingsViewCommand.Execute(null);
    }

    private void CalendarButton_OnClick(object? sender, RoutedEventArgs e)
    {
        (DataContext as MainViewModel)?.ToggleCalendarViewCommand.Execute(null);
    }
}