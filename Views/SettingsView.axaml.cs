using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using PomodoroTimer.Services;
using PomodoroTimer.ViewModels;

namespace PomodoroTimer.Views;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();
    }

    private void SaveSettingsButton_OnClick(object? sender, RoutedEventArgs e)
    {
        (DataContext as SettingsViewModel)?.SaveCommand.Execute(null);
    }

    private void ResetSettingsButton_OnClick(object? sender, RoutedEventArgs e)
    {
        (DataContext as SettingsViewModel)?.ResetSettingsCommand.Execute(null);
    }
}