///////////////////////////////////////////////////////////////////
//
// Project MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

using System;

namespace Youbiquitous.Minimo.Settings.Storage;


/// <summary>
/// Helper class descriptor
/// </summary>
public class StorageDescriptor
{
    private const string Selector_Live = "live";

    /// <summary>
    /// Current connection string to use (live/local/more)
    /// </summary>
    public string Selector { get; set; }

    /// <summary>
    /// Production connection string
    /// </summary>
    public string Live { get; set; }

    /// <summary>
    /// Local connection string
    /// </summary>
    public string Local { get; set; }

    /// <summary>
    /// Whether the DB selector is live
    /// </summary>
    /// <returns></returns>
    public bool IsLive()
    {
        return Selector != null && 
               Selector.Equals(Selector_Live, StringComparison.InvariantCultureIgnoreCase);
    }


    /// <summary>
    /// Return the currently selected connection string
    /// </summary>
    /// <returns></returns>
    public string Get()
    {
        return Selector.ToLower() switch
        {
            Selector_Live=> Live,
            _ => Local
        };
    }
}