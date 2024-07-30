///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//



using Youbiquitous.Minimo.Resources;


namespace Youbiquitous.Minimo.ViewModels.UI;

/// <summary>
/// List of methods to build dedicated and context-sensitive menus
/// </summary>
public class MenuBuilder
{
    /// <summary>
    /// For test purposes
    /// </summary>
    /// <returns></returns>
    public static Menu Home()
    {
        return new Menu()
            .Item(AppMenu.Home, "/home/index", "fa fa-home", "", "");
    
    }

    public static Menu Timesheet()
    {
        return new Menu()
            .Item(AppMenu.Home, "/work/timesheet", "far fa-list-ol", "", "");

    }

    public static Menu History()
    {
        return new Menu()
            .Item(AppMenu.Home, "/work/history", "far fa-list-ol", "", "");

    }
    public static Menu Diagram()
    {
        return new Menu()
            .Item(AppMenu.Home, "/work/diagram", "fa-solid fa-chart-pie", "", "");

    }

}