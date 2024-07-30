///////////////////////////////////////////////////////////////////
//
// Project MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Youbiquitous.Minimo.Settings.Core;

public class LocalizerSettings
{
    public LocalizerSettings()
    {
        Languages = new List<string>();
        Enabled = true;
    }

    /// <summary>
    /// Whether enabled 
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// List of supported languages
    /// </summary>
    public List<string> Languages { get; set; }

    /// <summary>
    /// Code of the default culture
    /// </summary>
    public string DefaultCultureCode { get; set; }



    /// <summary>
    /// Return the default culture as an object
    /// </summary>
    /// <returns></returns>
    public CultureInfo DefaultCulture()
    {
        return string.IsNullOrWhiteSpace(DefaultCultureCode)
            ? new CultureInfo("en-us")
            : new CultureInfo(DefaultCultureCode);
    }

    /// <summary>
    /// List of supported cultures
    /// </summary>
    /// <returns></returns>
    public IList<CultureInfo> GetSupportedCultures()
    {
        if (Languages == null)
            return new List<CultureInfo>();

        var list = new List<CultureInfo>();
        foreach (var language in Languages)
        {
            if (string.IsNullOrWhiteSpace(language))
                continue;
            list.Add(new CultureInfo(language));
        }

        return list;
    }

    /// <summary>
    /// List of currently languages to switch to
    /// </summary>
    /// <returns></returns>
    public IList<CultureInfo> GetAvailableLanguages()
    {
        var currentCulture = Thread.CurrentThread.CurrentUICulture;
        var list = GetSupportedCultures().ToList();
        list.RemoveAll(c => c.Name == currentCulture.Name);
        return list;
    }

    /// <summary>
    /// Wrapper to obtain the current UI culture
    /// </summary>
    /// <returns></returns>
    public CultureInfo CurrentCulture()
    {
        return Thread.CurrentThread.CurrentUICulture;
    }

    /// <summary>
    /// Localization active if at least 2 languages
    /// </summary>
    /// <returns></returns>
    public bool Active()
    {
        return Enabled && Languages.Count > 1;
    }
}