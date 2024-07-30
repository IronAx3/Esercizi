///////////////////////////////////////////////////////////////////
//
// Projects MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//


namespace Youbiquitous.Minimo.ViewModels.UI;

/// <summary>
/// Class for sidebar menu 
/// </summary>
public class SidebarMenu
{
    public SidebarMenu()
    {
        Items = new List<SidebarMenuItem>();
    }

    /// <summary>
    /// List of menu items
    /// </summary>
    public List<SidebarMenuItem> Items { get; }

    /// <summary>
    /// Menu default for the home page
    /// </summary>
    /// <returns></returns>
    public static SidebarMenu MenuForHomePage()
    {
        var menu = new SidebarMenu();
        menu.Items.AddRange(new List<SidebarMenuItem>
        {
            new("Home", "/"),
            new(),
        });   
            
        return menu;
    }


}