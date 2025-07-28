using System.Collections.Generic;

namespace PomodoroTimer.Models;

public class CalendarGoals
{
    public List<CalendarEntry> CalendarEntries { get; set; } = new List<CalendarEntry>();
}