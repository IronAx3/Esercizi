///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//

using Youbiquitous.Minimo.DomainModel.Account;
using Youbiquitous.Minimo.Settings.Core;
using Youbiquitous.Minimo.ViewModels;

namespace Youbiquitous.Minimo.ViewModels
{
    /// <summary>
    /// The model for editing the user profile
    /// </summary>
    public class UserViewModel : MainViewModelBase
    {
        public UserViewModel(string permissions, MinimoSettings settings) : base(permissions, settings)
        {
            IsEdit = false;
        }
        public bool IsEdit { get; set; }

        /// <summary>
        /// User record
        /// </summary>
        public UserAccount User { get; set; }



    }
}