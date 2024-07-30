using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Youbiquitous.Minimo.DomainModel.Account;
using Youbiquitous.Minimo.Settings.Core;

namespace Youbiquitous.Minimo.ViewModels.Account
{
    public class ProjectViewModel : MainViewModelBase
    {
       public ProjectViewModel(string permissions, MinimoSettings settings): base(permissions, settings)
        {
            IsEdit = false;
        }
        public bool IsEdit { get; set; }

        /// <summary>
        /// User record
        /// </summary>
        public Project Project { get; set; }

        public WorkType WorkType { get; set; }

        public Work DailyPercentage { get; set; }

    }
}
