///////////////////////////////////////////////////////////////////
//
// Project MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

namespace Youbiquitous.Minimo.Shared.Exceptions;

/// <summary>
/// Invalid role exception
/// </summary>
public class InvalidRoleException : MinimoException
{
    public InvalidRoleException(string message) : base(message)
    {
    }
}

