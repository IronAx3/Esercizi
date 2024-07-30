///////////////////////////////////////////////////////////////////
//
// Project MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//


using Youbiquitous.Martlet.Core.Extensions;

namespace Youbiquitous.Minimo.Shared.Extensions;

public static class MiscExtensions
{
    /// <summary>
    /// Assign a default value if null or whitespace
    /// </summary>
    /// <param name="theString"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static string Default(this string theString, string defaultValue)
    {
        if (theString.IsNullOrWhitespace())
            theString = defaultValue;

        return theString;
    }
}
