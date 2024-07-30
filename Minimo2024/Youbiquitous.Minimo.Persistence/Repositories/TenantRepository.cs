using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Youbiquitous.Minimo.DomainModel.Account;

namespace Youbiquitous.Minimo.Persistence.Repositories
{
	public partial class TenantRepository
	{

		public static string BlobConnectionString { get; set; }

		public TenantRepository(string blobConnStr)
		{
			BlobConnectionString = blobConnStr;
		}
		/// <summary>
		/// Find the tenant object by ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Tenant FindById(long id)
		{
			using var db = new MinimoDatabase();
			var tenant = (from c in db.Tenants.Include(u => u.DisplayName)
						where c.Id == id
						select c).SingleOrDefault();
			return tenant;
		}

		/// <summary>
		/// Retrieve tenant
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		public static Tenant FindByEmail(string email)
		{
			using var db = new MinimoDatabase();
			var tenant = (from u in db.Tenants
					.Include(u => u.Email)
						where u.Email == email
						select u).SingleOrDefault();
			return tenant;
		}


		/// <summary>
		/// Physical loader of records from the table to be held in memory
		/// </summary>
		/// <returns></returns>
		public List<Tenant> All()
		{
			using var db = new MinimoDatabase();
			var records = (from p in db.Tenants.Include(u => u.Name)
						   where !p.Deleted
						   select p).ToList();
			return records;
		}

		public IList<Tenant> AllUsers()
		{
			using var db = new MinimoDatabase();
			var records = (from p in db.Tenants.Include(u => u.RelatedUsers)
						   where !p.Deleted
						   select p).ToList();
			return records;
		}

	


	}
}
