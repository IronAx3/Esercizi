using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Youbiquitous.Minimo.DomainModel.Account;

namespace Youbiquitous.Minimo.Persistence.Repositories
{
	public partial class ProjectRepository
	{
		public static string BlobConnectionString { get; set; }

		public ProjectRepository(string blobConnStr)
		{
			BlobConnectionString = blobConnStr;
		}

		/// <summary>
		/// Find the project by ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Project FindById(long id)
		{
			using var db = new MinimoDatabase();
			var project = (from c in db.Projects
						   where c.Id == id
						   select c).SingleOrDefault();
			return project;
		}

		/// <summary>
		/// Physical loader of records from the table to be held in memory
		/// </summary>
		/// <returns></returns>
		public IList<Project> All()
		{
			using var db = new MinimoDatabase();
			var records = (from p in db.Projects
						   where !p.Deleted
						   select p).ToList();
			return records;
		}

		private List<Project> GetProjectsByDay(DateTime? specificDate)
		{
			using var db = new MinimoDatabase();
			var records = (from w in db.Works
						   where w.WorkDate == specificDate
						   select w)
						   .ToList();
			var projects = new List<Project>();
			foreach(var record in records)
			{
				var project = (from p in db.Projects
							where p.Works == record
							select p).FirstOrDefault();
				projects.Add(project);
			}
			return projects;
		}

		private List<Project> GetProjectsByDateRange(DateTime? startDate, DateTime? endDate)
		{
			using var db = new MinimoDatabase();

			var records = (from w in db.Works
						  where startDate <= w.WorkDate && w.WorkDate <= endDate
						   select w).ToList();
			var projects = new List<Project>();
			foreach (var record in records)
			{
				var project = (from p in db.Projects
							   where p.Works == record
							   select p).FirstOrDefault();
				projects.Add(project);
			}

			return projects;
		}

		public List<Project> GetProjectsBySpecificTime(DateTime? firstDate, DateTime? secondDate )
		{

			if(firstDate != null && secondDate == null)
			{
				return GetProjectsByDay(firstDate);

			}else if(firstDate!= null && secondDate!= null)
			{
				return GetProjectsByDateRange(firstDate, secondDate);
			}
			else
			{
				All();
			}


			return null;
		}


	}
}
