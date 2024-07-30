///////////////////////////////////////////////////////////////////
//
// Projects MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Youbiquitous.Minimo.Settings.Core;

namespace Youbiquitous.Minimo.App.Controllers;

public partial class TestController : MinimoController
{
    public TestController(MinimoSettings settings, ILoggerFactory loggerFactory, IHttpContextAccessor accessor) : base(settings, loggerFactory, accessor)
    {
    }

    [Route("/sso")]
    //[Authorize(AuthenticationSchemes = SsoAuthenticationDefaults.SsoAuthenticationScheme)] 
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)] 
    public IActionResult LogAsOperator([Bind(Prefix = "sso-token")] string token)
    {
        // return Content(token + "<hr>" + User.Identity.Name);
        var name = User;
        return LocalRedirect("/");
    }

    [Route("/test")]
    public IActionResult Sample()
    {
        return Content("OK");
    }
}