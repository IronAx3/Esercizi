///////////////////////////////////////////////////////////////////
//
// Projects MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

using Youbiquitous.Martlet.Core.Extensions;

namespace Youbiquitous.Minimo.ViewModels.UI;

/// <summary>
/// Class for menu items
/// </summary>
public class SidebarMenuItem
{
    public SidebarMenuItem()
    {
        IsDivider = true;
    }

    public SidebarMenuItem(string text, string url = "#", string icon = "", string target = "")
    {
        Text = text;
        Url = url;
        Icon = icon;
        Target = target;
        IsDivider = text.IsNullOrWhitespace();
    }

    public string Text { get; set; }
    public string Url { get; set; }
    public string Icon { get; set; }
    public bool IsDivider { get; }
    public string Target { get; set; }
}