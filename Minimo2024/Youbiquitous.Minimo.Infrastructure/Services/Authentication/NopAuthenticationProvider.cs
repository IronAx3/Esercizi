///////////////////////////////////////////////////////////////////
//
// Project MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//


using Youbiquitous.Minimo.Shared.Auth;

namespace Youbiquitous.Minimo.Infrastructure.Services.Authentication;

/// <summary>
/// Sample authentication provider just comparing email and password
/// </summary>
public class NopAuthenticationProvider : IAuthenticationProvider
{
    public NopAuthenticationProvider()
    {
        Name = "NOP";
    }

    /// <summary>
    /// Name of the provider
    /// </summary>
    public string Name { get; }

    public AuthenticationResponse ValidateCredentials(string email, string password)
    {
        return AuthenticationResponse.Fail();
    }

    public AuthenticationResponse ValidateCredentials(string ekey, string email, string password)
    {
        throw new NotImplementedException();
    }
}