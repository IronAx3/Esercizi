///////////////////////////////////////////////////////////////////
//
// Projects MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//


using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Minimo.Resources;
using Youbiquitous.Minimo.Settings.Core;
using Youbiquitous.Minimo.ViewModels.UI;

namespace Youbiquitous.Minimo.ViewModels;

public class MainViewModelBase
{
    protected MainViewModelBase(string title = "")
    {
        Title = title;
        Menu = new SidebarMenu();
        HeaderLayout = HeaderLayout.Default;
    }

    public MainViewModelBase(string permissions,MinimoSettings settings)
    {
        Title = "";
        ShowSearchBox = false;
        Menu = new SidebarMenu();
        Sidebar = SidebarBuilder.FromPermissions(permissions);
        Settings = settings;
    }
    public MainViewModelBase(string permissions,MinimoSettings settings, string title = "")
    {
        Title = title;
        ShowSearchBox = false;
        Menu = new SidebarMenu();
        Sidebar = SidebarBuilder.FromPermissions(permissions);
        Settings = settings;
    }

    #region PUBLIC FACTORIES
    public static MainViewModelBase Default(string title = "")
    {
        var model = new MainViewModelBase(title);
        return model;
    }

    public static MainViewModelBase Default(MinimoSettings settings, string title = "")
    {
        var model = new MainViewModelBase(title) { Settings = settings, Title = title };
        if (title.IsNullOrWhitespace())
            model.Title = settings.General.ApplicationName;
        return model;
    }
    #endregion


    /// <summary>
    /// Gets/sets the title of the overall HTML page 
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets/sets the name of the CSS class (theme) for the page background 
    /// </summary>
    public string Theme { get; private set; }

    /// <summary>
    /// Gets/sets the text at the top of the view 
    /// </summary>
    public string Header { get; set; }

    /// <summary>
    /// Gets/sets the text at the top of the view--second line  
    /// </summary>
    public string SubHeader { get; set; }

    /// <summary>
    /// How to lay out the two possible strings of the header text
    /// </summary>
    public HeaderLayout HeaderLayout { get; set; }

    /// <summary>
    /// Menu items
    /// </summary>
    public SidebarMenu Menu { get; set; }

    /// <summary>
    /// Gets/sets the settings of the application
    /// </summary>
    public MinimoSettings Settings { get; set; }

    /// <summary>
    /// Indicates whether the search box should be visible 
    /// </summary>
    public bool ShowSearchBox { get; set; }

    /// <summary>
    /// Content of the sidebar
    /// </summary>
    public Sidebar Sidebar { get; set; }

    /// <summary>
    /// Set the theme to use
    /// </summary>
    /// <param name="theme"></param>
    public void SetTheme(string theme)
    {
        Theme = theme.ToLower();
    }
}

public enum HeaderLayout
{
    Default = 0,
    TwoLines = 0,
    SameLine = 1

}

public static class HeaderLayoutExtensions
{
    public static bool IsDefault(this HeaderLayout layout)
    {
        return layout == HeaderLayout.Default;
    }
    public static bool IsSameLine(this HeaderLayout layout)
    {
        return layout == HeaderLayout.SameLine;
    }
}