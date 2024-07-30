using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Youbiquitous.Martlet.Core.Types;
using Youbiquitous.Minimo.DomainModel.Account;
using Youbiquitous.Minimo.Persistence.Repositories;
using Youbiquitous.Minimo.Settings.Core;
using Youbiquitous.Minimo.ViewModels.Account;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Youbiquitous.Minimo.Services.Account
{
	public class WorkService : ApplicationServiceBase
	{
		private readonly WorkRepository _workRepo;
		private readonly ProjectRepository _projectRepo;
		private readonly UserAccount _userRepo;

		public WorkService(MinimoSettings settings) : base(settings)
		{
			_workRepo = new(settings.Secrets.BlobStorageConnectionString);
			_projectRepo = new(settings.Secrets.BlobStorageConnectionString);
		}

		public WorkViewModel GetWorkViewModel(string permissions, long id = 0)
		{
			var isEdit = id > 0 ? true : false;
			var model = new WorkViewModel(permissions, Settings)
			{
				IsEdit = isEdit,
				//Work = isEdit ? _workRepo.FindById(id) : new Work()
				Work = _workRepo.FindById(id) ?? new Work(),
				Projects = _projectRepo.All()?.ToList(),
				Works = _workRepo.All()?.ToList()
			};
			return model;
		}
		/// <summary>
		/// Retrieves the record of given work 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Work FindById(long id)
		{
			return _workRepo.FindById(id);
		}

		/// <summary>
		/// List of works
		/// </summary>
		/// <param name="permissions"></param>
		/// <returns></returns>
		public WorksViewModel GetWorksViewModel(string permissions, string timeRange)
		{
			var model = new WorksViewModel(permissions, Settings)
			{
				Works = _workRepo.GetAllWorksByTimeRange(timeRange)
			};
			return model;
		}

		/// <summary>
		/// Save or Update a work
		/// </summary>
		/// <param name="date"></param>
		/// <param name="projectId"></param>
		/// <param name="workType"></param>
		/// <param name="percentage"></param>
		/// <param name="notes"></param>
		/// <param name="user"></param>
		/// <returns></returns>
		public CommandResponse Save(DateTime date, long projectId, WorkType workType, string percentage, string notes, long user)
		{
			//project.TenantId = 1;
			Work work = new Work
			{
				WorkDate = new DateTime(date.Year, date.Month, date.Day),
				ProjectId = projectId,
				WorkType = workType,
				DailyPercentage = percentage,
				Notes = notes,
				UserId = user
			};

			return _workRepo.Save(work);
		}

	

		public CommandResponse Delete(long id)
		{
			return _workRepo.Delete(id);
		}
	}
}
