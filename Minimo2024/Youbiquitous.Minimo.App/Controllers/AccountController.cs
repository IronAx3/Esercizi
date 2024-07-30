///////////////////////////////////////////////////////////////////
//
// Projects MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//


using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Martlet.Core.Types;
using Youbiquitous.Minimo.App.Common.Extensions;
using Youbiquitous.Minimo.App.Common.Startup;
using Youbiquitous.Minimo.DomainModel.Account;
using Youbiquitous.Minimo.Services.Account;
using Youbiquitous.Minimo.Services.Auth;
using Youbiquitous.Minimo.Settings.Core;
using Youbiquitous.Minimo.Shared.Auth;
using Youbiquitous.Minimo.ViewModels;
using Youbiquitous.Minimo.ViewModels.Account;

namespace Youbiquitous.Minimo.App.Controllers;

/// <summary>
/// Provides endpoints for the whole sign-in/sign-out process
/// </summary>
public partial class AccountController : MinimoController
{
    private readonly AuthService _authService;
    private readonly UserService _userService;
    private readonly WorkService _workService;

    public AccountController(MinimoSettings settings, ILoggerFactory loggerFactory,IHttpContextAccessor accessor)
        : base(settings, loggerFactory, accessor)
    {
        _authService = new AuthService(base.settings);
        _userService = new UserService(base.settings);
    }

    /// <summary>
    /// Displays the login page shared by all users of the system
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ActionName("login")]
    public IActionResult ShowSignInPage(string returnUrl)
    {
        var model = new LoginViewModel
        {
            Settings = settings,
            FormData =
            {
                ReturnUrl = returnUrl
            }
        };

        return View(ViewName(), model);
    }

    /// <summary>
    /// Validate provided credentials and logs the user in
    /// </summary>
    /// <param name="input">Values of the login input form</param>
    /// <returns>JSON command response</returns>
    [HttpPost]
    [ActionName("login")]
    public async Task<JsonResult> TrySignIn(LoginInput input)
    {
        var response = _authService.TryAuthenticate(input);
        if (!response.Success)
            return Json(CommandResponse.Fail().AddMessage(response.Message + "... error"));

        var redirectUrl = input.ReturnUrl.IsNullOrWhitespace() ? "/" : input.ReturnUrl;
        await HttpContext.AuthenticateUser(input.UserName, input.RememberMe);
        return Json(CommandResponse.Ok().AddRedirectUrl(redirectUrl));
    }

    /// <summary>
    /// Sign users out of the application
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("/logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return LocalRedirect("/");

    }

    /// <summary>
    /// Home page with the general dashboard
    /// </summary>
    /// <returns></returns>

    //[Route("Operator/Index")]
    //public IActionResult Index()
    //{
    //    var model =_workService.GetWorkViewModel(LoggedPermissions());
    //    return View(ViewName(), model);
    //}

    public IActionResult TimeSheet()
    {
        var model = _userService.GetUserViewModel(LoggedPermissions());
        return View(ViewName(), model);
    }



    public IActionResult ViewProfile()
    {
        var id = CurrentUser().Id;
        // Check it's the logged user; throw otherwise
        EnsureLoggedUser(id);
        var model = _userService.GetUserViewModel(LoggedPermissions(), id);
        return View(ViewName(), model);
    }

    /// <summary>
    /// Save a new contact or edit
    /// </summary>
    /// <param name="user"></param>
    /// <param name="headshot"></param>
    /// <param name="headshotIsDefined"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Save(UserAccount user)
    {
        var response = _userService.Save(user);
        return Json(response);
    }

    [HttpPost]
    public IActionResult Edit(long id = 0)
    {
        var model = _userService.GetUserViewModel(LoggedPermissions(), id);
        return View(ViewName(), model);
    }


}