///////////////////////////////////////////////////////////////////
//
// Projects MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//


namespace Youbiquitous.Minimo.ViewModels.Account;

/// <summary>
/// The input model type used to collect login data
/// </summary>
public class LoginInput
{
    /// <summary>
    /// The name of the user
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// The password (clear text) as the user typed it 
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Whether the user intends to stay connected
    /// </summary>
    public bool RememberMe { get; set; }

    /// <summary>
    /// URL to return to in case of direct access to a locked page
    /// </summary>
    public string ReturnUrl { get; set; }
}