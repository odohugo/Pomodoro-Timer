using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using PomodoroTimer.Services;

namespace PomodoroTimer;

public class App : Application
{
    public static WindowActivator WindowActivator; 
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainView();
            WindowActivator = new WindowActivator(desktop.MainWindow);
        }

        base.OnFrameworkInitializationCompleted();
    }
}