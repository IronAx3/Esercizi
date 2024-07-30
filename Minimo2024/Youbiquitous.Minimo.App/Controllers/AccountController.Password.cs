///////////////////////////////////////////////////////////////////
//
// Projects MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//


using Microsoft.AspNetCore.Mvc;
using Youbiquitous.Martlet.Core.Types;
using Youbiquitous.Minimo.Resources;
using Youbiquitous.Minimo.ViewModels;

namespace Youbiquitous.Minimo.App.Controllers;

/// <summary>
/// Provides endpoints for the whole sign-in/sign-out process
/// </summary>
public partial class AccountController 
{
    /// <summary>
    /// Present the page through which the user can request to recover a forgotten password
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("/forgot/password")]
    public IActionResult ShowForgotPasswordPage()
    {
        var model = SimpleViewModelBase.Default(settings);
        return View(ViewName("forgot_password"), model);
    }

    /// <summary>
    /// Send the reset link via email for a forgot-password scenario
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("/send/link/reset/password")]
    public IActionResult SendLinkForPassword(string email)
    {
        //var iso = Culture.TwoLetterISOLanguageName;
        //var response = _authService.TrySendLinkForPasswordReset(email, ServerUrl, iso);
        Thread.Sleep(2000);
        var response = CommandResponse.Ok().AddMessage($"An email was sent to <i>{email}</i>");
        return Json(response);
    }

    /// <summary>
    /// Present the page through which the user can reset his/her password
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("/account/reset/{token}")]
    [ActionName("reset")]
    public IActionResult DisplayResetPasswordView(string token)
    {
        //var model = new SetPasswordViewModel { Settings = Settings };

        //// Validate and route to retry view in case
        //var user = UserLoginRepository.FindByPasswordResetToken(token);
        //if (user == null)
        //    return View("resend", model);

        //// Was the token issued less than N seconds ago?
        //model.User = user;
        //model.Email = user.Email;
        //model.Token = token;
        //var view = user.IsPasswordResetTokenValid(Settings.General.ResetPasswordLifetimeSecs) 
        //    ? "reset" 
        //    : "resend";
        //return View(view, model);
        return Ok();
    }
        
    /// <summary>
    /// Resets the password for UserLogins
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ActionName("reset")]
    public IActionResult ResetPassword(string token, string password, string password2)
    {
        //if (!password.Equals(password2))
        //    return Json(CommandResponse.Fail()
        //        .AddMessage(AppErrors.Err_PasswordsDontMatch));

        //var response = _authService.TryResetPassword(token, password);
        //return Json(response);
        return Ok();
    }
}