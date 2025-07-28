using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using PomodoroTimer.Services;
using PomodoroTimer.ViewModels;

namespace PomodoroTimer.Views;

public partial class CalendarView : UserControl
{
    private CalendarService _calendarService;
    public CalendarView()
    {
        InitializeComponent();
        _calendarService = new CalendarService();
        PopulateCalendarGrid();
        this.DataContext = new CalendarViewModel();
    }

    private void PopulateCalendarGrid()
    {
        var currentDate = DateTime.Today;
        var startDate = currentDate.AddDays(-30);
        List<DateTime> dates = new List<DateTime>();
        for (int i = 0; i <= 30; i++)
        {
           dates.Add(startDate.AddDays(i)); 
        }
        
        var column = 0;
        var row = 0;
        foreach (var item in dates)
        {
            var dateBorder = new Border();
            dateBorder.Classes.Add("CalendarBorder");
            var dateButton = new StackPanel();
            dateBorder.Child = dateButton;
            dateButton.Children.Add(new TextBlock()
            {
                Text = item.Date.ToString("dd"),
                Classes = {"CalendarButtonDateText"}
            });
            var pomodoroCount = _calendarService.GetCalendarEntry(item);
            if (pomodoroCount != null)
            {
                dateButton.Children.Add(new TextBlock()
                {
                    Text = pomodoroCount.Pomodoros.ToString(),
                    Classes = {"CalendarButtonPomodoroText"}
                });
            }
            Grid.SetColumn(dateBorder, column);
            Grid.SetRow(dateBorder, row);
            CalendarGrid.Children.Add(dateBorder);
            if (column == 7)
            {
                row++;
                column = 0;
            }
            else {column++;}
        }
    }
}