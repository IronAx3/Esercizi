///////////////////////////////////////////////////////////////////
//
// Project MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//


using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Minimo.Resources;

namespace Youbiquitous.Minimo.DomainModel.Misc;

public class LastUpdate
{
    public LastUpdate(string author, DateTime when)
    {
        Author = author;
        When = when;
    }

    /// <summary>
    /// Who made the update
    /// </summary>
    public string Author { get; set; }

    /// <summary>
    /// Time of the update
    /// </summary>
    public DateTime When { get; set; }

    /// <summary>
    /// Date of last update in a displayable format
    /// </summary>
    /// <param name="format"></param>
    /// <returns></returns>
    public string DateForDisplay(string format = "d MMM yyyy")
    {
        return When.IsMinValue()
            ? AppCommons.Text_NA
            : When.ToStringOrEmpty(format);
    }

    /// <summary>
    /// Display
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return When.IsMinValue()
            ? AppCommons.Text_NA
            : $"{DateForDisplay()} ({Author})";
    }
}