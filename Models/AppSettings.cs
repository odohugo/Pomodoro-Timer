
namespace PomodoroTimer.Models;

public class AppSettings
{
    public int WorkDuration { get; set; } = 25;
    public int ShortBreakDuration { get; set; } = 5;
    public int LongBreakDuration { get; set; } = 15;
    public int PomodorosUntilLongBreak { get; set; } = 4;
    public bool AutomaticallyStartNextTimer { get; set; } = false;
}