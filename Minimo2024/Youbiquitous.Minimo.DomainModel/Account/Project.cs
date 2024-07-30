using Logico.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Youbiquitous.Minimo.DomainModel.Account
{
    public class Project : MinimoBaseEntity
    {
        public Project() { }
        public Project( string displayName, long tenantId)
        {
            DisplayName = displayName;
            TenantId = tenantId;

        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public long TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public string Company {  get; set; }
        /// <summary>
        /// Linked list of user projects
        /// </summary>
        public ICollection<UserProjectBinding> UserProjectBindings { get; set; }
        public ICollection<UserAccount> UserAccounts => UserProjectBindings?.Select(up => up.User).ToList();
		public ICollection<Work> Works {  get; set; }
        public void Import(Project project)
        {
            Id = project.Id;
            Name = project.Name;
            DisplayName = project.DisplayName;
            TenantId = project.TenantId;
            Tenant = project.Tenant;
            Company = project.Company;
            UserProjectBindings = project.UserProjectBindings;
           
        }
    }
}
