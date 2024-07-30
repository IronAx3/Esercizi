
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Youbiquitous.Minimo.DomainModel.Account;
using Youbiquitous.Minimo.Settings.Core;
using Youbiquitous.Minimo.ViewModels;


namespace Youbiquitous.Minimo.ViewModels
{
    public class UsersViewModel : MainViewModelBase
    {
        public UsersViewModel(string permissions, MinimoSettings settings) : base(permissions, settings) { }
        public List<UserAccount> Users { get; set; }
        public List<Role> Roles { get; set; }
        public List<Project> Projects { get; set; }

    }
}
