using Avalonia.Controls;
using Avalonia.Threading;

namespace PomodoroTimer.Services;

public class WindowActivator
{
    private readonly Window _window;

    public WindowActivator(Window window)
    {
        _window = window;
    }

    public void BringToFront()
    {
        Dispatcher.UIThread.InvokeAsync(() =>
        {
            _window.WindowState = WindowState.Normal;
            _window.Topmost = true;
            _window.Activate();
            _window.Focus();
            _window.Topmost = false;
        });
    }
}