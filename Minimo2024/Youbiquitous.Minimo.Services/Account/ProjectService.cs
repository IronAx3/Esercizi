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
using Youbiquitous.Minimo.ViewModels.Account;

namespace Youbiquitous.Minimo.Services.Account
{
    public class ProjectService : ApplicationServiceBase
    {
        private readonly ProjectRepository _projectRepo;
	

        public ProjectService(MinimoSettings settings):base(settings)
        {
            _projectRepo = new(settings.Secrets.BlobStorageConnectionString);
        }

        public ProjectViewModel GetProjectViewModel(string permissions, long id = 0)
        {
            var isEdit = id > 0 ? true : false;
            var model = new ProjectViewModel(permissions, Settings)
            {
                IsEdit = isEdit,
                Project = isEdit ? _projectRepo.FindById(id) : new Project(),
			};
            return model;
        }
        /// <summary>
        /// Retrieves the record of given user 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Project FindById(long id)
        {
            return _projectRepo.FindById(id);
        }

        public ProjectsViewModel GetProjectsViewModel(string permissions)
        {
            var model = new ProjectsViewModel(permissions, Settings)
            {
                Projects = _projectRepo.All()?.ToList(),

            };
            return model;
        }

        public CommandResponse Save(Project project)
        {
            return _projectRepo.Save(project);
        }

        public CommandResponse Delete(long id)
        {
            return _projectRepo.Delete(id);
        }
    }
}
