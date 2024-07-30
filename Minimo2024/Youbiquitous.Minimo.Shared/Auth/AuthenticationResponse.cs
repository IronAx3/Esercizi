///////////////////////////////////////////////////////////////////
//
// Project MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

using Youbiquitous.Martlet.Core.Extensions;

namespace Youbiquitous.Minimo.Shared.Auth;

public class AuthenticationResponse
{
    /// <summary>
    /// Helper method for failed authentication
    /// </summary>
    /// <returns></returns>
    public static AuthenticationResponse Fail()
    {
        return new AuthenticationResponse(false);
    }

    /// <summary>
    /// Helper method for successful authentication
    /// </summary>
    /// <returns></returns>
    public static AuthenticationResponse Ok()
    {
        return new AuthenticationResponse(true);
    }

    /// <summary>
    /// General-purpose constructor
    /// </summary>
    /// <param name="success"></param>
    /// <param name="provider"></param>
    /// <param name="role"></param>
    public AuthenticationResponse(bool success, string provider = null, string role = null)
    {
        Success = success;
        AuthenticatedBy = provider;
    }

    /// <summary>
    /// Add the name of auth provider to the response
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public AuthenticationResponse AddSignature(string provider)
    {
        if (!provider.IsNullOrWhitespace())
            AuthenticatedBy = provider;
        return this;
    }

    /// <summary>
    /// Add the expected role of the authorized user within Corinto 
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public AuthenticationResponse AddRole(long role)
    {
        if (role == 0)
            Role = role;
        return this;
    }

    /// <summary>
    /// Add the expected email of the authorized user within Corinto 
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public AuthenticationResponse AddEmail(string email)
    {
        if (!email.IsNullOrWhitespace())
            Email = email;
        return this;
    }

    /// <summary>
    /// Add the expected display name of the account
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public AuthenticationResponse AddDisplayName(string name)
    {
        if (!name.IsNullOrWhitespace())
            DisplayName = name;
        return this;
    }

    /// <summary>
    /// Add ID (if any)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public AuthenticationResponse AddLoginId(long id)
    {
        LoginId = id;
        return this;
    }

    /// <summary>
    /// Add URL to redirect after authentication (used for multiple logins)
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public AuthenticationResponse AddRedirectUrl(string url)
    {
        if (!url.IsNullOrWhitespace())
            RedirectUrl = url;
        return this; 
    }

    /// <summary>
    /// Add a return message for the user
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public AuthenticationResponse AddMessage(string text)
    {
        if (!text.IsNullOrWhitespace())
            Message = text.Trim().TrimEnd('-');
        return this;
    }

    /// <summary>
    /// Even if authentication failed, the chain should stop here
    /// </summary>
    /// <returns></returns>
    public AuthenticationResponse Break()
    {
        ShouldBreak = true;
        return this;
    }
        
    /// <summary>
    /// Read properties
    /// </summary>
    public bool Success { get; }
    public string AuthenticatedBy { get; private set; }
    public long LoginId { get; private set; }
    public long Role { get; private set; }
    public string Email { get; private set; }
    public string DisplayName { get; private set; }
    public string Message { get; private set; }
    public bool ShouldBreak { get; private set; }
    public string RedirectUrl { get; private set; }
}