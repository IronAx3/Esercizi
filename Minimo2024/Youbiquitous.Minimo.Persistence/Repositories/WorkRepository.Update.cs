using Microsoft.EntityFrameworkCore;
using Youbiquitous.Martlet.Core.Types;
using Youbiquitous.Minimo.DomainModel.Account;
using Youbiquitous.Minimo.Resources;

namespace Youbiquitous.Minimo.Persistence.Repositories
{
	public partial class WorkRepository
	{
		public Work FindById(long id, bool includeInfo = false)
		{
			using var db = new MinimoDatabase();
			var work = includeInfo
				? db.Works.Include(w => w.Id)
				.Where(x => !x.Deleted)
				.SingleOrDefault(x => x.Id == id)
				: db.Works.Where(x => !x.Deleted)
				.SingleOrDefault(x => x.Id == id);
			return work;
		}

		/// <summary>
		/// Creates a new work
		/// </summary>
		/// <param name="db"></param>
		/// <param name="work"></param>
		/// <returns></returns>
		private CommandResponse CreateInternal(MinimoDatabase db, Work work)
		{
			if (work.Id > 0)
				return CommandResponse.Fail();
			try
			{
				db.Works.Add(work);
				db.SaveChanges();
				return CommandResponse.Ok().AddKey(work.Id.ToString());
			}
			catch
			{
				return CommandResponse.Fail();
			}
		}
		private CommandResponse UpdateInternal(MinimoDatabase db, Work found, Work work)
		{
			found.Import(work);
			try
			{
				db.SaveChanges();
				return CommandResponse.Ok().AddKey(work.Id.ToString());
			}
			catch
			{
				return CommandResponse.Fail();
			}
		}
		public CommandResponse Save(Work work)
		{
			if (work == null || work.Id < 0)
				return CommandResponse.Fail().AddMessage(AppMessages.Err_UserNotFound);
			using var db = new MinimoDatabase();
			var found = (from u in db.Works
						 where u.Id == work.Id && !u.Deleted
						 select u).SingleOrDefault();

			return found == null
					? CreateInternal(db, work)
					: UpdateInternal(db, found, work);
		}
		public CommandResponse Delete(long Id)
		{
			using var db = new MinimoDatabase();
			var found = db.Works.FirstOrDefault(x => x.Id == x.Id);
			if (found == null)
				return CommandResponse.Fail();
			try
			{
				db.Works.Remove(found);
				db.SaveChanges();
				return CommandResponse.Ok();
			}
			catch (Exception ex)
			{
				return CommandResponse.Fail();
			}
		}
		public CommandResponse SoftDelete(long id)
		{
			using var db = new MinimoDatabase();
			var found = db.Works.SingleOrDefault(x => x.Id == id);
			if (found == null)
				return CommandResponse.Fail();
			try
			{
				found.SoftDelete();
				db.SaveChanges();
				return CommandResponse.Ok();
			}
			catch (Exception ex)
			{
				return CommandResponse.Fail();
			}
		}

		
	}
}