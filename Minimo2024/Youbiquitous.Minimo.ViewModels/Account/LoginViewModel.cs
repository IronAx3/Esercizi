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
public class LoginViewModel : LandingViewModelBase
{
    public LoginViewModel()
    {
        FormData = new LoginInput();
    }

    /// <summary>
    /// Login form data
    /// </summary>
    public LoginInput FormData { get; set; }
}