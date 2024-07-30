using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Youbiquitous.Minimo.DomainModel.Account;
using Youbiquitous.Minimo.Settings.Core;

namespace Youbiquitous.Minimo.ViewModels.Account
{
	public class WorksViewModel : MainViewModelBase
	{
		public WorksViewModel(string permissions, MinimoSettings settings) : base(permissions, settings) { }
		public List<Work> Works { get; set; }


	}
}
