///////////////////////////////////////////////////////////////////
//
// Project MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//


using Youbiquitous.Martlet.Services.Security.Password;
using Youbiquitous.Minimo.Resources;
using Youbiquitous.Minimo.Shared.Auth;

namespace Youbiquitous.Minimo.Infrastructure.Services.Authentication;

/// <summary>
/// Sample authentication provider just comparing email and password of USER-LOGINs
/// </summary>
public class MinimoAuthenticationProvider : IAuthenticationProvider
{
    /// <summary>
    /// Name of the provider
    /// </summary>
    public static string Name => "BUSINESS";

    /// <summary>
    /// Entry point for validating credentials
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public AuthenticationResponse ValidateCredentials(string email, string password)
    {
        return ValidateCredentialsInternal(email, password, skipPasswordCheck: false);
    }

    /// <summary>
    /// Entry point for validating credentials (NOT SUPPORTED IN THIS CONTEXT)
    /// </summary>
    /// <param name="ekey"></param>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public AuthenticationResponse ValidateCredentials(string ekey, string email, string password)
    {
        throw new Exception("Not supported action");
    }


    /// <summary>
    /// Authenticate w/o password check 
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <param name="skipPasswordCheck"></param>
    /// <returns></returns>
    public AuthenticationResponse ValidateCredentialsInternal(string email, string password, bool skipPasswordCheck)
    {
        // Give it a try
        var response = FindMatchingUser(email, password, skipPasswordCheck);
        return response;
    }

    /// <summary>
    /// Try to authenticate as a regular user
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <param name="skipPasswordCheck"></param>
    /// <returns></returns>
    private static AuthenticationResponse FindMatchingUser(string email, string password, bool skipPasswordCheck = false)
    {
        var passwordService = PasswordServiceLocator.Get();

        var candidate = AuthRepository.FindByEmail(email);
        if (candidate == null)
            return AuthenticationResponse.Fail()
                .AddMessage($"{AppErrors.Err_InvalidCredentials}");
        if (!skipPasswordCheck)
        {
            if (!passwordService.Validate(password, candidate.Password))
                return AuthenticationResponse.Fail()
                    .AddMessage(AppErrors.Err_InvalidCredentials);
        }

        var response = AuthenticationResponseBuilder.BuildFrom(candidate, Name);
        return response;
    }
}