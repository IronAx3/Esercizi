///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//



using System.Collections.Generic;
using Youbiquitous.Minimo.DomainModel.Account;
using Youbiquitous.Minimo.ViewModels.UI;

namespace Youbiquitous.Minimo.ViewModels.UI;

/// <summary>
/// List of methods to build dedicated and context-sensitive sidebars
/// </summary>
public class SidebarBuilder
{
    public static Sidebar FromPermissions(string permissions)
    {
        var options = new List<Menu>();

        options.Add(MenuBuilder.Home());

        // Compose menus
        return new Sidebar().AddMenu(options.ToArray());
    }

    public static Sidebar ForUser(UserAccount user)
    {
        var options = new List<Menu>();

        options.Add(MenuBuilder.Home());
        options.Add(MenuBuilder.Timesheet());
        options.Add(MenuBuilder.History());
        options.Add(MenuBuilder.Diagram());

        if(user.IsSystem())
            options.Add(MenuBuilder.Home());

        // Compose menus
        return new Sidebar().AddMenu(options.ToArray());
    }
}