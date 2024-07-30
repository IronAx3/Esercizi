///////////////////////////////////////////////////////////////////
//
// Projects MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Youbiquitous.Minimo.DomainModel.Account;
using Youbiquitous.Minimo.Services.Auth;

namespace Youbiquitous.Minimo.App.Common.Extensions;

public static class CookieExtensions
{
    /// <summary>
    /// Retrieve the user from the claims in the presented principal
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static UserAccount Logged(this ClaimsPrincipal user)
    {
        var email = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        //var role = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        var member = AuthService.FindUser(email);
        return member;
    }

    /// <summary>
    /// Creates the auth cookie for a regular user of the application
    /// </summary>
    /// <param name="context"></param>
    /// <param name="username"></param>
    /// <param name="role"></param>
    /// <param name="rememberMe"></param>
    /// <returns></returns>
    public static async Task AuthenticateUser(this HttpContext context, string username, /*string role,*/ bool rememberMe)
    {
        await CreateCookieInternal(context, username, /*role, */rememberMe);
    }

    /// <summary>
    /// Actual code to create the DONATO cookie
    /// </summary>
    /// <param name="context"></param>
    /// <param name="email"></param>
    /// <param name="role"></param>
    /// <param name="rememberMe"></param>
    /// <returns></returns>
    private static async Task CreateCookieInternal(HttpContext context, string email, /*string role,*/ bool rememberMe)
    {
        // Create the authentication cookie
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, email),
            new Claim(ClaimTypes.Role, "")
        };
        var identity = new ClaimsIdentity(claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

        await context.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity),
            new AuthenticationProperties
            {
                IsPersistent = rememberMe, 
                RedirectUri = "/",
                AllowRefresh = true
            });
    }
}