using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Youbiquitous.Martlet.Core.Types;
using Youbiquitous.Minimo.DomainModel.Account;
using Youbiquitous.Minimo.Resources;

namespace Youbiquitous.Minimo.Persistence.Repositories
{
	public partial class WorkRepository
	{
		public static string BlobConnectionString { get; set; }

		public WorkRepository(string blobConnStr)
		{
			BlobConnectionString = blobConnStr;
		}

		/// <summary>
		/// Find the work by ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Work FindById(long id)
		{
			using var db = new MinimoDatabase();
			var work = (from c in db.Works
						   where c.Id == id	  
						   select c).SingleOrDefault();
			return work;
		}

		/// <summary>
		/// Physical loader of records from the table to be held in memory
		/// </summary>
		/// <returns></returns>
		public List<Work> All()
		{
			using var db = new MinimoDatabase();
			var records = (from p in db.Works.Include(p=> p.Project)
						   where !p.Deleted
						   select p).ToList();
			return records;
		}
	   /// <summary>
	   /// 	List of Works by time range
	   /// </summary>
	   /// <param name="timeRange"></param>
	   /// <returns></returns>
		public List<Work> GetAllWorksByTimeRange(string timeRange)
		{
			var startDate = DateTime.Now;
			var endDate = DateTime.Now;

			switch (timeRange)
			{
				case "Current Day":
					startDate = DateTime.Today;
					endDate = DateTime.Today;
					break;
				case "Current Week":
					startDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
					endDate = startDate.AddDays(7);
					break;
				case "Current Month":
					startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
					endDate = startDate.AddMonths(1).AddTicks(-1);
					break;
				case "2 Months":
					startDate = DateTime.Today.AddMonths(-2);
					endDate = DateTime.Today.AddDays(1);
					break;
				case "3 Months":
					startDate = DateTime.Today.AddMonths(-3);
					endDate = DateTime.Today.AddDays(1);
					break;
				case "6 Months":
					startDate = DateTime.Today.AddMonths(-6);
					endDate = DateTime.Today.AddDays(1);
					break;
				case "Current Year":
					startDate = new DateTime(DateTime.Today.Year, 1, 1);
					endDate = new DateTime(DateTime.Today.Year + 1, 1, 1);
					break;
				default:
					break;
			}
			using var db = new MinimoDatabase();
			var works = db.Works.Include(w=> w.Project)
					   .Where(w => w.WorkDate >= startDate && w.WorkDate <= endDate)
					   .OrderBy(w => w.WorkDate)
					   .ToList();

			return works;
		}

		
	}
}

