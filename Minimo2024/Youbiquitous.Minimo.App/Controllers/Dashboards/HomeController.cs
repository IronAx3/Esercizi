///////////////////////////////////////////////////////////////////
//
// Projects MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Youbiquitous.Minimo.Settings.Core;
using Youbiquitous.Minimo.ViewModels;
using Youbiquitous.Minimo.ViewModels.Account;

namespace Youbiquitous.Minimo.App.Controllers;

public class HomeController : MinimoController

{
	public HomeController(MinimoSettings settings) : base(settings) { }

	/// <summary>
	/// Home page
	/// </summary>
	/// <returns></returns>
	[Authorize]
	public IActionResult Index()
	{
		var model = MainViewModelBase.Default(settings);
		return View(ViewName(), model);
	}
}