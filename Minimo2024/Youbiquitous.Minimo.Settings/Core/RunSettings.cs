///////////////////////////////////////////////////////////////////
//
// Project MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

namespace Youbiquitous.Minimo.Settings.Core;

public class RunSettings
{
    public RunSettings()
    {
        DevMode = true;
        Theme = "default";
    }

    /// <summary>
    /// Whether the app should be considered in dev mode regardless of runtime environment
    /// </summary>
    public bool DevMode { get; set; }

    /// <summary>
    /// Whether the browser info box should be displayed
    /// </summary>
    public bool ShowBrowserLiveInfo { get; set; }

    /// <summary>
    /// Width of the day window for upcoming tournaments to show in the UI
    /// </summary>
    public int TournamentDaysAhead { get; set; }

    /// <summary>
    /// Name of the app-specific theme
    /// </summary>
    public string Theme { get; set; }
}