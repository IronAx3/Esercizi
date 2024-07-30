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
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Minimo.App.Common.Startup;

namespace Youbiquitous.Minimo.App.Controllers;

/// <summary>
/// Provides endpoints for the whole sign-in/sign-out process
/// </summary>
public partial class AccountController : MinimoController
{
    /// <summary>
    /// Azure AD signin/redirect WTA
    /// </summary>
    /// <returns></returns>
    [Route("/wta/signin")]
    public async Task SigninWithWtaAd()
    {
        var props1 = new AuthenticationProperties { RedirectUri = "" };
        SignOut(props1, AzureAdConstants.WtaOpenIdScheme);

        // AzureAD will call you back here once done
        var props2 = new AuthenticationProperties { RedirectUri = "/wta/redirect" };
        await HttpContext.ChallengeAsync(AzureAdConstants.WtaOpenIdScheme, props2);
    }

    /// <summary>
    /// Run back from Azure AD authentication
    /// </summary>
    /// <returns></returns>
    [Route("/wta/redirect")]
    public async Task<IActionResult> BackFromWta()
    {
        // Collect the temporary cookie created for us by the external authentication module
        var authenticateResult = await HttpContext.AuthenticateAsync(AzureAdConstants.WtaTempCookieScheme);
        var principal = authenticateResult.Principal;
        if (principal == null)
            return LocalRedirect("/");

        // Read email, fname and lname from AD login
        var email = principal.Identity?.Name; // Email as username
        var fname = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
        var lname = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
        if (email == null || !email.Contains("@"))
        {
            email = principal.Claims.FirstOrDefault(c => c.Type == "emails")?.Value;
            if (email == null || !email.Contains("@"))
                return LocalRedirect("/");
        }

        if (fname.IsNullOrWhitespace() && lname.IsNullOrWhitespace())
        {
            try
            {
                fname = principal.Claims.FirstOrDefault(c => c.Type == "name")?.Value?.SubstringTo(" ");
                lname = principal.Claims.FirstOrDefault(c => c.Type == "name")?.Value?.SubstringFrom(" ");
            }
            catch (Exception e)
            {
                fname = "-";
                lname = "-";
            }
        }

        if (fname.IsNullOrWhitespace())
            fname = "-";

        if (lname.IsNullOrWhitespace())
            lname = "-";

        //AuthenticateUser(HttpContext, user, true);

        // Get rid of the temporary cookie
        await HttpContext.SignOutAsync(AzureAdConstants.WtaTempCookieScheme);

        // Done!
        return LocalRedirect("/");
    }
}