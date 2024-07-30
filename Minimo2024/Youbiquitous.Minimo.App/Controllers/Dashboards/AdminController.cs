///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Youbiquitous.Minimo.App.Controllers;
using Youbiquitous.Minimo.DomainModel.Account;
using Youbiquitous.Minimo.Settings.Core;
using Youbiquitous.Minimo.ViewModels;

namespace Logico.App.Controllers.Dashboards
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    //[EnsureRole(Role.System, Role.Admin)]
    public class AdminController : MinimoController
    {
        public AdminController(MinimoSettings settings, ILoggerFactory loggerFactory, IHttpContextAccessor accessor)
            : base(settings, loggerFactory, accessor) {}

        /// <summary>
        /// Home page for ADMIN users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View("/index", MainViewModelBase.Default(LoggedPermissions()));
        }
    }
}