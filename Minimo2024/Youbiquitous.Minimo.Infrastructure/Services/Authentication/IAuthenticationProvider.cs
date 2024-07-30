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
/// Overall interface for authentication providers trying to validate credentials
/// </summary>
public interface IAuthenticationProvider
{
    public static string Name { get; }
    AuthenticationResponse ValidateCredentials(string email, string password);
    AuthenticationResponse ValidateCredentials(string ekey, string email, string password);
}