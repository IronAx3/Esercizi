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
/// MFA support
/// </summary>
public class AccountMfa  
{
    /// <summary>
    /// When the user requested a MFA token 
    /// </summary>
    public DateTime? Requested { get; set; }

    /// <summary>
    /// MFA token 
    /// </summary>
    public Guid Token { get; set; }
}