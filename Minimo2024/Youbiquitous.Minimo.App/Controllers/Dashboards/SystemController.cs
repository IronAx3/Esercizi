///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Youbiquitous.Minimo.App.Controllers;
using Youbiquitous.Minimo.DomainModel.Account;
using Youbiquitous.Minimo.Services.Account;
using Youbiquitous.Minimo.Settings.Core;

namespace Logico.App.Controllers.Dashboards
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    //[EnsureRole(Role.System)]
    public class SystemController : MinimoController
    {
        private readonly UserService _userService;
        public SystemController(MinimoSettings settings, ILoggerFactory loggerFactory, IHttpContextAccessor accessor)
            : base(settings, loggerFactory, accessor)
        {
            _userService = new(settings);
        }

        /// <summary>
        /// Home page for SYSTEM users
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var model = _userService.GetUsersViewModel(LoggedPermissions());
            return View(model);
        }
    }
}