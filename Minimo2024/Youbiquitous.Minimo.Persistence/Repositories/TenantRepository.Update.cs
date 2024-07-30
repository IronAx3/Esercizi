using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Martlet.Core.Types;
using Youbiquitous.Martlet.Services.Security.Password;
using Youbiquitous.Martlet.Services.Security;
using Youbiquitous.Minimo.DomainModel.Account;
using Youbiquitous.Minimo.Resources;

namespace Youbiquitous.Minimo.Persistence.Repositories
{
	public partial class TenantRepository
	{

		private readonly IPasswordService _password = PasswordServiceLocator.Get();
		public CommandResponse SaveProfile(Tenant tenant)
		{
			// Check
			if (tenant == null || tenant.Id < 0)
				return CommandResponse.Fail().AddMessage(AppErrors.Err_UserNotFound);

			// Find persona ref
			using var db = new MinimoDatabase();
			var found = (from u in db.Tenants
						 where u.Id == tenant.Id && !u.Deleted
						 select u).SingleOrDefault();

			if (found == null)
				return CommandResponse.Fail().AddMessage(AppErrors.Err_UserNotFound);

			found.Import(tenant);
			try
			{
				db.SaveChanges();
				return CommandResponse.Ok().AddMessage(AppErrors.Success_OperationCompleted);
			}
			catch (Exception)
			{
				return CommandResponse.Fail().AddMessage(AppErrors.Err_OperationFailed);
			}
		}
		/// <summary>
		/// Inserts or updates a tenant record
		/// </summary>
		/// <param name="persona"></param>
		/// <param name="author"></param>
		/// <returns></returns>
		public CommandResponse Save(Tenant tenant)
		{
			// Check
			if (tenant == null || tenant.Id < 0)
				return CommandResponse.Fail().AddMessage(AppErrors.Err_UserNotFound);

			using var db = new MinimoDatabase();
			var found = (from u in db.Tenants
						 where u.Id == tenant.Id && !u.Deleted
						 select u).SingleOrDefault();

			return found == null
				? InsertInternal(db, tenant)
				: UpdateUserInternal(db, found, tenant);
		}
		public CommandResponse Delete(long id)
		{
			// Find persona ref
			using var db = new MinimoDatabase();
			var found = (from u in db.Tenants
						 where u.Id == id
						 select u).SingleOrDefault();
			if (found == null)
				return CommandResponse.Fail().AddMessage(AppErrors.Err_UserNotFound);

			try
			{
				db.Tenants.Remove(found);
				db.SaveChanges();
				return CommandResponse.Ok().AddMessage(AppErrors.Success_OperationCompleted);
			}
			catch (Exception)
			{
				return CommandResponse.Fail().AddMessage(AppErrors.Err_OperationFailed);
			}
		}
		public CommandResponse SoftDelete(long id)
		{
			using var db = new MinimoDatabase();
			var found = db.Tenants.SingleOrDefault(x => x.Id == id);
			if (found == null)
				return CommandResponse.Fail();
			try
			{
				found.SoftDelete();
				db.SaveChanges();
				return CommandResponse.Ok();
			}
			catch
			{
				return CommandResponse.Fail();
			}
		}
		private static CommandResponse InsertInternal(MinimoDatabase db, Tenant tenant)
		{
			tenant.Name = tenant.Name.Capitalize();
		
			#region ADD TENANT
			db.Tenants.Add(tenant);
			try
			{
				db.SaveChanges();
			}
			catch (Exception)
			{
				return CommandResponse.Fail().AddMessage(AppErrors.Err_OperationFailed);
			}
			#endregion
			return CommandResponse.Ok().AddMessage(AppErrors.Success_OperationCompleted);
		}
		/// <summary>
		/// Update ALL PROFILE FIELDS of first record with info from second record
		/// </summary>
		/// <param name="db"></param>
		/// <param name="found"></param>
		/// <param name="persona"></param>
		/// <param name="author"></param>
		/// <param name="profileOnly"></param>
		/// <returns></returns>
		private static CommandResponse UpdateUserInternal(MinimoDatabase db, Tenant found, Tenant tenant)
		{
			found.Import(tenant);
			try
			{
				db.SaveChanges();
			}
			catch (Exception)
			{

				return CommandResponse.Fail().AddMessage(AppErrors.Err_OperationFailed);
			}
			return CommandResponse.Ok().AddMessage(AppErrors.Success_OperationCompleted);
		}

		/// <summary>
		/// Update PASSWORD of given tenant
		/// </summary>
		/// <param name = "db" ></ param >
		/// < param name="found"></param>
		/// <param name = "password" ></ param >
		/// < param name="author"></param>
		/// <returns></returns>
		private CommandResponse UpdatePasswordInternal(MinimoDatabase db, Tenant tenant, string password, string author)
		{
			tenant.Password = _password.Store(password);
			tenant.LatestPasswordChange = DateTime.UtcNow;
			tenant.Mark(isEdit: true, author);

			try
			{
				db.SaveChanges();
			}
			catch (Exception)
			{

				return CommandResponse.Fail().AddMessage(AppErrors.Err_OperationFailed);
			}
			return CommandResponse.Ok();
		}
	

		/// <summary>
		/// Sets a new password
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="oldPassword"></param>
		/// <param name="newPassword"></param>
		/// <param name="author"></param>
		/// <returns></returns>
		public CommandResponse ChangePassword(long tenantId, string oldPassword, string newPassword, string author = null)
		{
			// Check
			if (oldPassword == null || tenantId <= 0)
				return CommandResponse.Fail().AddMessage(AppErrors.Err_UserNotFound);

			using var db = new MinimoDatabase();
			var found = (from u in db.Tenants
						 where u.Id == tenantId && !u.Deleted
						 select u).SingleOrDefault();
			if (found == null)
				return CommandResponse.Fail().AddMessage(AppErrors.Err_UserNotFound);

			// Verify old password matches
			if (!_password.Validate(oldPassword, found.Password))
				return CommandResponse.Fail().AddMessage(AppErrors.Err_UserNotFound);

			return UpdatePasswordInternal(db, found, newPassword, author);
		}

		/// <summary>
		/// Overwrite old password for given tenant
		/// </summary>
		/// <param name="tenantId"></param>
		/// <param name="newPassword"></param>
		/// <param name="author"></param>
		/// <returns></returns>
		public CommandResponse ResetPassword(long tenantId, string newPassword, string author = null)
		{
			// Check
			if (tenantId <= 0)
				return CommandResponse.Fail().AddMessage(AppErrors.Err_UserNotFound);

			// Find persona ref
			using var db = new MinimoDatabase();
			var found = (from u in db.Tenants
						 where u.Id == tenantId && !u.Deleted
						 select u).SingleOrDefault();
			if (found == null)
				return CommandResponse.Fail().AddMessage(AppErrors.Err_UserNotFound);

			return UpdatePasswordInternal(db, found, newPassword, author);
		}

		/// <summary>
		/// Mark the tenant record to receive a password reset
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		public Tenant MarkForPasswordReset(string email)
		{
			using var db = new MinimoDatabase();
			var user = (from u in db.Tenants
						where u.Email == email && !u.Deleted
						select u).SingleOrDefault();
			if (user == null)
				return null;

			user.PasswordResetRequest = DateTime.UtcNow;
			user.PasswordResetToken = Guid.NewGuid().ToString("N"); // Just numbers
			db.SaveChanges();
			return user;
		}

		/// <summary>
		/// Verify the given password reset token exists and returns matching record
		/// </summary>
		/// <param name="token"></param>
		/// <returns></returns>
		public static Tenant FindByPasswordToken(string token)
		{
			if (token.IsNullOrWhitespace())
				return null;

			using var db = new MinimoDatabase();
			var found = (from u in db.Tenants
						 where u.PasswordResetToken == token
						 select u).SingleOrDefault();
			return found;
		}

		/// <summary>
		/// Reset the password if the token is still valid
		/// </summary>
		/// <param name="token"></param>
		/// <param name="newPassword"></param>
		/// <returns></returns>
		public CommandResponse ResetPasswordByToken(string token, string newPassword)
		{
			// Check
			if (token.IsNullOrWhitespace() || newPassword.IsNullOrWhitespace())
				return CommandResponse.Fail();

			using var db = new MinimoDatabase();
			var found = (from u in db.Tenants
						 where u.PasswordResetToken == token && !u.Deleted
						 select u).SingleOrDefault();
			if (found == null)
				return CommandResponse.Fail();

			return UpdatePasswordInternal(db, found, newPassword, found.Email);
		}
	}
}
