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
/// Members related to change of password, confirmation and MFA
/// </summary>
public partial class UserAccount
{
    public ChangePassword ChangePassword { get; set; }
    public AccountMfa Mfa { get; set; }
    public AccountConfirmation Confirmation { get; set; }
}

