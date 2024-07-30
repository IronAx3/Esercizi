///////////////////////////////////////////////////////////////////
//
// Project MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

namespace Youbiquitous.Minimo.Shared.Exceptions;


public enum ExceptionHandlingMode
{
    Auto = 0,
    Development = 1,
    Production = 2
}

/// <summary>
/// Ad hoc extension methods
/// </summary>
public static class ExceptionHandlingModeExtensions
{
    public static bool IsAuto(this ExceptionHandlingMode mode)
    {
        return mode == ExceptionHandlingMode.Auto;
    }
    public static bool IsDevelopment(this ExceptionHandlingMode mode)
    {
        return mode == ExceptionHandlingMode.Development;
    }
    public static bool IsProduction(this ExceptionHandlingMode mode)
    {
        return mode == ExceptionHandlingMode.Production;
    }
    public static bool Is(this ExceptionHandlingMode mode, ExceptionHandlingMode other)
    {
        return mode == other;
    }
}