///////////////////////////////////////////////////////////////////
//
// Projects MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Json;
using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Martlet.Mvc.Core;
using Youbiquitous.Minimo.App.Common.Extensions;
using Youbiquitous.Minimo.DomainModel.Account;
using Youbiquitous.Minimo.Resources;
using Youbiquitous.Minimo.Settings.Core;
using Youbiquitous.Minimo.ViewModels.UI;

namespace Youbiquitous.Minimo.App.Controllers;

public class MinimoController : Controller
{

    /// <summary>
    /// Gets and shares application settings
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="loggerFactory"></param>
    /// <param name="accessor"></param>
    public MinimoController(MinimoSettings settings, ILoggerFactory loggerFactory, IHttpContextAccessor accessor)
    {
        this.settings = settings;
        Logger = loggerFactory?.CreateLogger(settings.General.ApplicationName);
        HttpConnection = accessor;
    }

    public MinimoController(MinimoSettings settings)
    {
        this.settings = settings;
    }

    /// <summary>
    /// Gain access to HTTP connection info
    /// </summary>
    protected IHttpContextAccessor HttpConnection { get; }

    /// <summary>
    /// Gain access to application settings
    /// </summary>
    protected MinimoSettings settings { get; }

    /// <summary>
    /// Centralized logging reference 
    /// </summary>
    protected ILogger Logger { get; }

    /// <summary>
    /// Email of the currently logged user
    /// </summary>
    protected string CurrentUserEmail =>  User.Logged().Email;

    /// <summary>
    /// Simplify retrieving views and controllers
    /// </summary>
    /// <param name="plainViewName"></param>
    /// <returns></returns>
    public string ViewName(string plainViewName = null)
    {
        if (plainViewName.IsNullOrWhitespace())
            plainViewName = ControllerContext.RouteData.Values["action"]?.ToString();

        var controller = GetType().Name.SubstringTo("controller");
        return $"{RazorPaths.Pages}/{controller}/{plainViewName}.cshtml";
    }

    /// <summary>
    /// Simplify retrieving views from given controller name
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="plainViewName"></param>
    /// <returns></returns>
    public string ViewName(string controller, string plainViewName)
    {
        if (plainViewName.IsNullOrWhitespace())
            plainViewName = ControllerContext.RouteData.Values["action"]?.ToString();
        if (controller == null)
            controller = GetType().Name.SubstringTo("controller");

        return $"{RazorPaths.Pages}/{controller}/{plainViewName}.cshtml";
    }

    /// <summary>
    /// Current server 
    /// </summary>
    protected string ServerUrl => $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

    /// <summary>
    /// Current culture
    /// </summary>
    protected CultureInfo Culture => HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture;

    /// <summary>
    /// Gain access to the currently logged account
    /// </summary>
    /// <returns></returns>
    protected UserAccount CurrentUser()
    {
        return User?.Logged();
    }

    /// <summary>
    /// Signature of the user to be saved in CRUD ops
    /// </summary>
    /// <returns></returns>
    protected string Signature()
    {
        return User?.Logged().Email;
    }

    protected string LoggedPermissions()
    {
        return User?.Logged().SerializedPermissions;
    }

    /// <summary>
    /// Check whether the current account has the given ID
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    protected bool IsCurrentUser(string email)
    {
        var user = CurrentUser();
        if (user == null)
            return false;
        if (user.Email.IsNullOrWhitespace())
            return false;
        return user.Email.EqualsAny(email);
    }

    /// <summary>
    /// Package up remote connection information
    /// </summary>
    /// <returns></returns>
    public RemoteCaller Connection()
    {
        return RemoteCaller.Build(HttpConnection);
    }

    /// <summary>
    /// Checks whether the currently logged user is the same for which the request is made 
    /// </summary>
    /// <param name="id"></param>
    protected void EnsureLoggedUser(long id)
    {
        if (User == null || id < 0)
            throw new UnauthorizedAccessException(AppMessages.Err_UnauthorizedOperation);

        if (User.Logged().Id != id)
            throw new UnauthorizedAccessException(AppMessages.Err_UnauthorizedOperation);
    }

    /// <summary>
    /// Helper to return plain JSON with no property naming convention applied
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public IActionResult JsonIgnoreCase(object data)
    {
        return Json(data, new JsonSerializerOptions { PropertyNamingPolicy = null });
    }




}