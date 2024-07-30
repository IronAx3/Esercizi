
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Youbiquitous.Martlet.Core.Types;
using Youbiquitous.Minimo.DomainModel.Account;
using Youbiquitous.Minimo.Persistence.Repositories;
using Youbiquitous.Minimo.Resources;
using Youbiquitous.Minimo.Settings.Core;
using Youbiquitous.Minimo.ViewModels;

namespace Youbiquitous.Minimo.Services.Account
{
    public class UserService : ApplicationServiceBase
    {
        private readonly UserRepository _userRepo;

        public UserService(MinimoSettings settings) : base(settings)
        {
            _userRepo = new(settings.Secrets.BlobStorageConnectionString);
        }
        public UserViewModel GetUserViewModel(string permissions, long id = 0)
        {
            var isEdit = id > 0 ? true : false;
            var model = new UserViewModel(permissions, Settings)
            {
                IsEdit = isEdit,
                User = isEdit ? _userRepo.FindById(id) : new UserAccount(),
  
			};
            return model;
        }
        /// <summary>
        /// Retrieves the record of given user 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserAccount FindById(long id)
        {
            return _userRepo.FindById(id);
        }

        /// <summary>
        /// Update a user password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        /// <param name="oldPassword"></param>
        /// <param name="author"></param>
        /// <returns></returns>
        public CommandResponse ChangePassword(long userId, string oldPassword, string newPassword, string author)
        {
            var response = _userRepo.ChangePassword(userId, oldPassword, newPassword, author);
            if (response.Success)
                response.AddMessage(AppMessages.Success_PasswordChanged);
            return response;
        }

        public UsersViewModel GetUsersViewModel(string permissions)
        {
            var model = new UsersViewModel(permissions,Settings)
            {
                Users = _userRepo.All()?.ToList()
            };
            return model;
        }

        public CommandResponse Save(UserAccount user)
        {
            return _userRepo.Save(user);
        }

        public CommandResponse SaveProfile(UserAccount user)
        {
            return _userRepo.SaveProfile(user);
        }

        public CommandResponse Delete(long id)
        {
            return _userRepo.Delete(id);
        }


    }
}
