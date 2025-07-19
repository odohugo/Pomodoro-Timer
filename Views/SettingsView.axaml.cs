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
        this.DataContext = new SettingsViewModel(new SettingsService());
    }

    private void SaveSettingsButton_OnClick(object? sender, RoutedEventArgs e)
    {
        (DataContext as SettingsViewModel)?.SaveCommand.Execute(null);
    }

}