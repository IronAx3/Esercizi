using Logico.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youbiquitous.Minimo.DomainModel.Account
{
    public class Work : MinimoBaseEntity
    {
        #region Primary Key
        public long Id { get; set; }
        #endregion

        #region Foreign Key
        public long UserId { get; set; }
        public UserAccount User { get; set; }
        public long ProjectId { get; set; }
        public Project Project { get; set; }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public DateTime WorkDate { get; set; }

        /// <summary>
        /// user type work
        /// </summary>
        public WorkType WorkType { get; set; }

        /// <summary>
        /// time percentage on a project
        /// </summary>
        public string DailyPercentage { get; set; }

		public string? Notes { get; set; }

		public void Import(Work work)
        {
            User = work.User;
            Project = work.Project;
            WorkDate = work.WorkDate;
            WorkType = work.WorkType;
            DailyPercentage = work.DailyPercentage;
            Notes = work.Notes;
                        
        } 


    }
}
