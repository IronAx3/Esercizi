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

public class AuthenticationChain
{
    private readonly IList<IAuthenticationProvider> _authenticationChain;

    public AuthenticationChain()
    {
        _authenticationChain = new List<IAuthenticationProvider>();
    }

    /// <summary>
    /// Populates the list of authentication providers
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public AuthenticationChain Add(IAuthenticationProvider provider)
    {
        if (provider != null)
            _authenticationChain.Add(provider);

        return this;
    }

    /// <summary>
    /// Evaluates listed provider against provided credentials
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public AuthenticationResponse Evaluate(string email, string password)
    {
        foreach (var provider in _authenticationChain)
        {
            var response = provider.ValidateCredentials(email, password);
            return response;
        }

        return AuthenticationResponse.Fail();
    }

    /// <summary>
    /// Evaluates listed provider against provided credentials (in the given event)
    /// </summary>
    /// <param name="ekey"></param>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public AuthenticationResponse Evaluate(string ekey, string email, string password)
    {
        foreach (var provider in _authenticationChain)
        {
            var response = provider.ValidateCredentials(ekey, email, password);
            return response;
        }

        return AuthenticationResponse.Fail();
    }
}