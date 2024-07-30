///////////////////////////////////////////////////////////////////
//
// Project MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

namespace Youbiquitous.Minimo.DomainModel.Account;

/// <summary>
/// Change password
/// </summary>
public class AccountConfirmation
{
    /// <summary>
    /// When a new user account was requested
    /// </summary>
    public DateTime? Requested { get; set; }

    /// <summary>
    /// Request token
    /// </summary>
    public Guid Token { get; set; }

    /// <summary>
    /// When the user account was created/confirmed 
    /// </summary>
    public DateTime? Confirmed { get; set; }
}