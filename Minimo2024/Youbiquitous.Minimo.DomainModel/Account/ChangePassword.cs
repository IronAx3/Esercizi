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
public class ChangePassword
{
    /// <summary>
    /// When the password change was requested 
    /// </summary>
    public DateTime? Requested { get; set; }

    /// <summary>
    /// Token for password change 
    /// </summary>
    public Guid Token { get; set; }

    /// <summary>
    /// When the password change was finalized
    /// </summary>
    public DateTime? Confirmed { get; set; }
}