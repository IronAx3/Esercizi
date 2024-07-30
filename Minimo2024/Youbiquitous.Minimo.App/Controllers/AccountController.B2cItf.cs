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
using Microsoft.AspNetCore.Mvc;
using Youbiquitous.Minimo.App.Common.Extensions;
using Youbiquitous.Minimo.App.Common.Startup;
using Youbiquitous.Minimo.Services.Auth;

namespace Youbiquitous.Minimo.App.Controllers;

/// <summary>
/// Azure AD signin/redirect ITF
/// </summary>
public partial class AccountController : MinimoController
{
    /// <summary>
    /// Entry point to the ITF Azure Active Directory 
    /// </summary>
    /// <returns></returns>
    [Route("/itf/signin")]
    public async Task SigninWithItfAd()
    {
        var props1 = new AuthenticationProperties { RedirectUri = "" };
        SignOut(props1, AzureAdConstants.ItfOpenIdScheme);

        // AzureAD will call you back here once done
        var props2 = new AuthenticationProperties { RedirectUri = "/itf/redirect" };
        await HttpContext.ChallengeAsync(AzureAdConstants.ItfOpenIdScheme, props2); 
    }

    /// <summary>
    /// Run back from Azure AD authentication
    /// </summary>
    /// <returns></returns>
    //[Route("/itf/redirect")]
    //[HttpGet]
    //public async Task<IActionResult> BackFromItf()
    //{
    //    // Collect the auth info created for us by the external authentication module
    //    var authenticateResult = await HttpContext.AuthenticateAsync(AzureAdConstants.ItfTempCookieScheme); 
    //    var principal = authenticateResult.Principal;
    //    if (principal == null)
    //        return LocalRedirect("/");

    //    // Read email, fname and lname from AD login
    //    var email = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
    //    var tennisId = principal.Claims.FirstOrDefault(c => c.Type == "tennisId")?.Value;

    //    // Get rid of the temporary cookie
    //    await HttpContext.SignOutAsync(AzureAdConstants.ItfTempCookieScheme);

    //    // Create a new user on the fly
    //    AuthService.CreateUser(email, tennisId);
    //    await HttpContext.AuthenticateUser(email, true);

    //    // Done!
    //    return Redirect("/");
    //}
}