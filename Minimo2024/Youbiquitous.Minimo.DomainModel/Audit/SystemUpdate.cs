///////////////////////////////////////////////////////////////////
//
// Project MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

using System.ComponentModel.DataAnnotations;
using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Minimo.DomainModel.Misc;
using Youbiquitous.Minimo.Resources;

namespace Youbiquitous.Minimo.DomainModel.Audit;

public class SystemUpdate
{
    public static SystemUpdate New(string author, string action, string description = null)
    {
        return new SystemUpdate
        {
            Author = author,
            Action = action,
            When = DateTime.UtcNow,
            Description  = description
        };
    }

    public static SystemUpdate Empty()
    {
        return new SystemUpdate
        {
            Author = null,
            Action = null,
            When = DateTime.MinValue
        };
    }

    /// <summary>
    /// Unique system identifier of the record
    /// </summary>
    public long Id { get; set; }     

    /// <summary>
    /// Email of the user who performed the update
    /// </summary>
    [MaxLength(60)]
    public string Author { get; set; }

    /// <summary>
    /// Time of the update
    /// </summary>
    public DateTime When { get; set; }

    /// <summary>
    /// Type of update
    /// </summary>
    [MaxLength(60)]
    public string Action { get; set; }

    /// <summary>
    /// Optional description of the update (and reason)
    /// </summary>
    [MaxLength(200)]
    public string Description { get; set; }

    /// <summary>
    /// Extract last-update details
    /// </summary>
    /// <returns></returns>
    public LastUpdate ToRef()
    {
        return new LastUpdate(Author, When);
    }

    /// <summary>
    /// Default text representation
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return When.ToStringOrEmpty("d MMM", AppCommons.Text_NA);
    }
}

/// <summary>
/// List of system-wide traceable operations
/// </summary>
public static class SystemUpdateAction
{
    //public static readonly string TournamentDownload = AppOps.Action_TournamentDownload;
    //public static readonly string TournamentZap = AppOps.Action_TournamentZap;
}