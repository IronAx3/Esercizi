///////////////////////////////////////////////////////////////////
//
// Projects MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Minimo.Settings.Core;

namespace Youbiquitous.Minimo.ViewModels;

public class LandingViewModelBase
{
    protected LandingViewModelBase(string title = "")
    {
        Title = title;
    }

    #region PUBLIC FACTORIES
    public static LandingViewModelBase Default(string title = "")
    {
        var model = new LandingViewModelBase(title);
        return model;
    }

    public static LandingViewModelBase Default(MinimoSettings settings, string title = "")
    {
        var model = new LandingViewModelBase(title) { Settings = settings, Title = title };
        if (title.IsNullOrWhitespace())
            model.Title = settings.General.ApplicationName;
        return model;
    }
    #endregion


    /// <summary>
    /// Gets/sets the title of the view 
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets/sets the settings of the application
    /// </summary>
    public MinimoSettings Settings { get; set; }

    /// <summary>
    /// Indicates whether the model is in a valid state
    /// </summary>
    /// <returns></returns>
    public virtual bool IsValid()
    {
        return true;
    }
}