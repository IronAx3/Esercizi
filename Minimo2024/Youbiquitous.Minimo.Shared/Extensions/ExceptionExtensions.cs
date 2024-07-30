///////////////////////////////////////////////////////////////////
//
// Project MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//


namespace Youbiquitous.Minimo.Shared.Extensions;

public static class ExceptionExtensions
{
    /// <summary>
    /// Combine together message and inner-exception message
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public static string FullMessage(this Exception exception)
    {
        return $"{exception.Message}|{exception.InnerException?.Message}".Trim('|');
    }
}
