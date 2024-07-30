using Microsoft.AspNetCore.Mvc;
using Youbiquitous.Minimo.App.Common.Extensions;
using Youbiquitous.Minimo.DomainModel.Account;
using Youbiquitous.Minimo.Services.Account;
using Youbiquitous.Minimo.Services.Auth;
using Youbiquitous.Minimo.Settings.Core;
using Youbiquitous.Minimo.ViewModels.Account;

namespace Youbiquitous.Minimo.App.Controllers
{
	public class WorkController : MinimoController
	{
		private readonly WorkService _workService;
		private readonly AuthService _authService;

		public WorkController(MinimoSettings settings, ILoggerFactory loggerFactory, IHttpContextAccessor accessor)
			: base(settings, loggerFactory, accessor)
		{
			_workService = new WorkService(base.settings);
		}


		/// <summary>
		/// Home page with the general dashboard
		/// </summary>
		/// <returns></returns>
		[Route("Operator/Index")]
		public IActionResult Index()
		{
			var model = _workService.GetWorkViewModel(LoggedPermissions());
			return View(ViewName(), model);
		}

		/// <summary>
		/// User's Timesheet
		/// </summary>
		/// <returns></returns>
		[Route("Operator/TimeSheet")]
		public IActionResult TimeSheet()
		{

			var model = _workService.GetWorkViewModel(LoggedPermissions());
			return View(ViewName(), model);
		}

		/// <summary>
		/// Load all works
		/// </summary>
		/// <returns></returns>
        [Route("Operator/History/")]
        public IActionResult History(string timeRange = "Current Week")
        {
            var model = _workService.GetWorksViewModel(LoggedPermissions(),timeRange);
            return View(ViewName(), model);
        }

        /// <summary>
        /// Save a new work or edit
        /// </summary>
        /// <param name="work"></param>
        /// <param name="headshot"></param>
        /// <param name="headshotIsDefined"></param>
        /// <returns></returns>
        [HttpPost]
		public IActionResult Save(DateTime date, long projectId, WorkType workType, string percentage, string notes)
		{
			var user = User.Logged().Id;
			var response = _workService.Save(date, projectId,workType, percentage, notes, user);

            var model = _workService.GetWorkViewModel(LoggedPermissions());
            //return View("/Views/Pages/Work/timesheet.cshtml", model);
			return Json(response);
        }

		/// <summary>
		/// Load modal in Timesheet
		/// </summary>
		/// <param name="workId"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult LoadTimesheetModal(long workId = 0)
		{
			var model = _workService.GetWorkViewModel(LoggedPermissions(), workId);
			
			return PartialView("/Views/Pages/Work/pv_timesheet_modal.cshtml", model);
		}



	}
}
