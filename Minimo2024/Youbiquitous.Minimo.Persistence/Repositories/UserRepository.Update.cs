using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Martlet.Core.Types;
using Youbiquitous.Martlet.Services.Security;
using Youbiquitous.Minimo.Resources;
using Youbiquitous.Minimo.DomainModel.Account;
using Youbiquitous.Martlet.Services.Security.Password.Hashing;
using Azure.Storage.Blobs;
using Youbiquitous.Martlet.Services.Security.Password;

namespace Youbiquitous.Minimo.Persistence.Repositories
{
    public partial class UserRepository
    {
        private readonly IPasswordService _password = PasswordServiceLocator.Get();
        public CommandResponse SaveProfile(UserAccount user)
        {
            // Check
            if (user == null || user.Id < 0)
                return CommandResponse.Fail().AddMessage(AppErrors.Err_UserNotFound);

            // Find persona ref
            using var db = new MinimoDatabase();
            var found = (from u in db.Users
                         where u.Id == user.Id && !u.Deleted
                         select u).SingleOrDefault();

            if (found == null)
                return CommandResponse.Fail().AddMessage(AppErrors.Err_UserNotFound);

            found.Import(user);
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
        /// Inserts or updates a user record
        /// </summary>
        /// <param name="persona"></param>
        /// <param name="author"></param>
        /// <returns></returns>
        public CommandResponse Save(UserAccount user)
        {
            // Check
            if (user == null || user.Id < 0)
                return CommandResponse.Fail().AddMessage(AppErrors.Err_UserNotFound);

            // Find persona ref
            using var db = new MinimoDatabase();
            var found = (from u in db.Users
                         where u.Id == user.Id && !u.Deleted
                         select u).SingleOrDefault();

            return found == null
                ? InsertInternal(db, user)
                : UpdateUserInternal(db, found, user);
        }
        public CommandResponse Delete(long id)
        {
            // Find persona ref
            using var db = new MinimoDatabase();
            var found = (from u in db.Users
                         where u.Id == id
                         select u).SingleOrDefault();
            if (found == null)
                return CommandResponse.Fail().AddMessage(AppErrors.Err_UserNotFound);

            try
            {
                db.Users.Remove(found);
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
            var found = db.Users.SingleOrDefault(x => x.Id == id);
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
        private static CommandResponse InsertInternal(MinimoDatabase db, UserAccount user)
        {
            user.FirstName = user.FirstName.Capitalize();
            user.LastName = user.LastName.Capitalize();

            #region ADD USER
            db.Users.Add(user);
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                return CommandResponse.Fail().AddMessage(AppErrors.Err_OperationFailed);
            }
            #endregion
            return CommandResponse.Ok().AddMessage(AppErrors.Success_OperationCompleted).AddKey(user.Id.ToString());
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
        private static CommandResponse UpdateUserInternal(MinimoDatabase db, UserAccount found, UserAccount user)
        {
            found.Import(user);
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {

                return CommandResponse.Fail().AddMessage(AppErrors.Err_OperationFailed);
            }
            return CommandResponse.Ok().AddMessage(AppErrors.Success_OperationCompleted).AddKey(found.Id.ToString());
        }

        /// <summary>
        /// Update PASSWORD of given user
        /// </summary>
        /// <param name = "db" ></ param >
        /// < param name="found"></param>
        /// <param name = "password" ></ param >
        /// < param name="author"></param>
        /// <returns></returns>
        private CommandResponse UpdatePasswordInternal(MinimoDatabase db, UserAccount user, string password, string author)
        {
            user.Password = _password.Store(password);
            user.LatestPasswordChange = DateTime.UtcNow;
            user.Mark(isEdit: true, author);

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
        private static string SaveToBlobStorage(BlobContainerClient container, Stream data, string name)
        {
            var blob = container.GetBlobClient(name);
            blob.DeleteIfExists();
            blob.Upload(data);
            return blob.Uri.AbsoluteUri;
        }

        /// <summary>
        /// Sets a new password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <param name="author"></param>
        /// <returns></returns>
        public CommandResponse ChangePassword(long userId, string oldPassword, string newPassword, string author = null)
        {
            // Check
            if (oldPassword == null || userId <= 0)
                return CommandResponse.Fail().AddMessage(AppErrors.Err_UserNotFound);

            // Find persona ref
            using var db = new MinimoDatabase();
            var found = (from u in db.Users
                         where u.Id == userId && !u.Deleted
                         select u).SingleOrDefault();
            if (found == null)
                return CommandResponse.Fail().AddMessage(AppErrors.Err_UserNotFound);

            // Verify old password matches
            if (!_password.Validate(oldPassword, found.Password))
                return CommandResponse.Fail().AddMessage(AppErrors.Err_UserNotFound);

            return UpdatePasswordInternal(db, found, newPassword, author);
        }

        /// <summary>
        /// Overwrite old password for given user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        /// <param name="author"></param>
        /// <returns></returns>
        public CommandResponse ResetPassword(long userId, string newPassword, string author = null)
        {
            // Check
            if (userId <= 0)
                return CommandResponse.Fail().AddMessage(AppErrors.Err_UserNotFound);

            // Find persona ref
            using var db = new MinimoDatabase();
            var found = (from u in db.Users
                         where u.Id == userId && !u.Deleted
                         select u).SingleOrDefault();
            if (found == null)
                return CommandResponse.Fail().AddMessage(AppErrors.Err_UserNotFound);

            return UpdatePasswordInternal(db, found, newPassword, author);
        }

        /// <summary>
        /// Mark the user record to receive a password reset
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public UserAccount MarkForPasswordReset(string email)
        {
            using var db = new MinimoDatabase();
            var user = (from u in db.Users
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
        public static UserAccount FindByPasswordToken(string token)
        {
            if (token.IsNullOrWhitespace())
                return null;

            using var db = new MinimoDatabase();
            var found = (from u in db.Users
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

            // Find persona ref
            using var db = new MinimoDatabase();
            var found = (from u in db.Users
                         where u.PasswordResetToken == token && !u.Deleted
                         select u).SingleOrDefault();
            if (found == null)
                return CommandResponse.Fail();

            return UpdatePasswordInternal(db, found, newPassword, found.Email);
        }

    }
}
