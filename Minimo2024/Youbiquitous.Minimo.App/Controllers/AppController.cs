///////////////////////////////////////////////////////////////////
//
// Projects MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Youbiquitous.Minimo.Settings.Core;
using Youbiquitous.Minimo.Shared.Exceptions;
using Youbiquitous.Minimo.ViewModels;

//using Crionet.Donato.Shared.Exceptions;

namespace Youbiquitous.Minimo.App.Controllers;

public class AppController : MinimoController
{
    public AppController(MinimoSettings settings)
        : base(settings)
    {
    }


    /// <summary>
    /// Sets the language cookie and returns to the requesting page
    /// </summary>
    /// <param name="cultureName"></param>
    /// <param name="returnUrl"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [Route("lang/{id}")]
    public IActionResult Lang(
        [Bind(Prefix = "id")] string cultureName,
        [Bind(Prefix = "r")] string returnUrl = "/")
    {
        var culture = new RequestCulture(cultureName);
        var cookie = CookieRequestCultureProvider.MakeCookieValue(culture);
        Response.Cookies.Append(
            MinimoSettings.CultureCookieName,
            cookie,
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

        return LocalRedirect(returnUrl);
    }

    /// <summary>
    /// Selectively returns the CSS to apply for the given configuration (in run-settings) 
    /// </summary>
    /// <returns></returns>
    [Route("/app/css")]
    [HttpGet]
    public IActionResult GetCss()
    {
        var cssFile = $"/css/{settings.General.ApplicationName}-theme-{settings.Run.Theme}.min.css";
        return File(cssFile.ToLower(), "text/css");
    }

    /// <summary>
    /// Systemwide error handler
    /// </summary>
    /// <returns></returns>
    public IActionResult Error()
    {
        var model = new ErrorViewModel
        {
            Settings = settings,
            Title = settings.General.ApplicationName
        };

        // Retrieve error information
        var error = HttpContext.Features.Get<IExceptionHandlerFeature>();
        if (error == null)
            return View(ViewName(), model);

        // Retrieve path information
        var path = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
        if (path == null)
            return View(ViewName(), model);

        // Store error details
        model.Path = path.Path;
        model.SetError(error.Error);

        // Log the error
        //Logger.LogInformation(error.Error.Message);

        return View(ViewName(), model);
    }

    /// <summary>
    /// Just for testing purposes, throws an exception
    /// </summary>
    /// <returns></returns>
    [Route("/throw")]
    public IActionResult Throw()
    {
        throw new MinimoException("Just a test exception")
            .AddRecoveryLink("Google", "https://google.com");
    }
}