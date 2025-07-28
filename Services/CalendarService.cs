using System.IO;
using System.Runtime.InteropServices.Swift;
using System.Text.Json;
using PomodoroTimer.Models;
using DateTime = System.DateTime;
namespace PomodoroTimer.Services;

public class CalendarService
{
   private DateTime _dateTime = new DateTime();
   private const string CalendarFileName = "calendar.json";

   public CalendarGoals CalendarGoals { get; set; }

   public CalendarService()
   {
      Load();
   }

   public CalendarEntry? GetCalendarEntry(DateTime date)
   {
      return CalendarGoals.CalendarEntries.Find( x => x.Date == date);
   }

   private void Load()
   {
      if (File.Exists(CalendarFileName))
      {
         var json = File.ReadAllText(CalendarFileName);
         CalendarGoals = JsonSerializer.Deserialize<CalendarGoals>(json) ?? new CalendarGoals();
      }
      else
      {
         CalendarGoals = new CalendarGoals();
      }
   }

   private void Save()
   {
      var json = JsonSerializer.Serialize(CalendarGoals);
      File.WriteAllText(CalendarFileName, json);
   }

   public void SaveDay(DateTime date, int pomodoros)
   {
      var calendarEntry = new CalendarEntry();
      calendarEntry.Date = date;
      calendarEntry.Pomodoros = pomodoros;
      var dateAlreadyExists = false;
      foreach (var entry in CalendarGoals.CalendarEntries)
      {
         if (entry.Date == date)
         {
            entry.Pomodoros = pomodoros;
            dateAlreadyExists = true;
            break;
         }
      }

      if (!dateAlreadyExists)
      {
         CalendarGoals.CalendarEntries.Add(calendarEntry);
      }
      Save();
   }
   
   public void ClearHistory()
   {
      CalendarGoals.CalendarEntries.Clear();
      Save();
   }
}