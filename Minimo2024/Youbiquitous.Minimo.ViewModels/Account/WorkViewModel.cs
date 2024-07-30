using Logico.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Youbiquitous.Minimo.DomainModel.Account;
using Youbiquitous.Minimo.Settings.Core;

namespace Youbiquitous.Minimo.ViewModels.Account
{
    public class WorkViewModel  : MainViewModelBase
    {
        public WorkViewModel(string permissions, MinimoSettings settings) : base(permissions, settings) 
		{
		}

		public bool IsEdit { get; set; }
		public Work Work {  get; set; }
        public List<Work> Works { get; set; }

        public Project Project { get; set; }
		public List<Project> Projects { get; set; }

		public UserAccount User { get; set; }

		

	}
}
