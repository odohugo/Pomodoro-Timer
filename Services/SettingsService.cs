using System;
using System.IO;
using System.Text.Json;
using PomodoroTimer.Models;

namespace PomodoroTimer.Services;

public class SettingsService
{
    private const string SettingsFileName = "settings.json";
    public AppSettings AppSettings { get; set; }

    public SettingsService()
    {
        Load();
    }

    public void Load()
    {
        if (File.Exists(SettingsFileName))
        {
            var json = File.ReadAllText(SettingsFileName);
            AppSettings = JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
        }
        else
        {
            AppSettings = new AppSettings();
        }
    }

    public void Save()
    {
        var json = JsonSerializer.Serialize(AppSettings);
        File.WriteAllText(SettingsFileName, json);
    }
    
}