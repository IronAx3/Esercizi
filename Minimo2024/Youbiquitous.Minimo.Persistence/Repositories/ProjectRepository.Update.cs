using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Youbiquitous.Martlet.Core.Types;
using Youbiquitous.Minimo.DomainModel.Account;
using Youbiquitous.Minimo.Resources;

namespace Youbiquitous.Minimo.Persistence.Repositories
{
    public partial class ProjectRepository
    {
        public Project FindById(long id, bool includeInfo = false)
        {
            using var db = new MinimoDatabase();
            var project = includeInfo
                ? db.Projects.Include(p => p.Name)
                .Where(x => !x.Deleted)
                .SingleOrDefault(x => x.Id == id)
                : db.Projects.Where(x => !x.Deleted)
                .SingleOrDefault(x => x.Id == id);
            return project;
        }
        public List<Project> FindAll(bool includeInfo = false)
        {
            using var db = new MinimoDatabase();
            var list = includeInfo
                ? db.Projects.Include(x => x.Name).Include(x=> x.UserProjectBindings)
                .Where(x => !x.Deleted)
                .ToList()
                : db.Projects.Where(x => !x.Deleted)
                .ToList();

            return list;
        }
        private CommandResponse CreateInternal(MinimoDatabase db, Project project)
        {
            if (project.Id > 0)
                return CommandResponse.Fail();
            try
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return CommandResponse.Ok().AddKey(project.Id.ToString());
            }
            catch (Exception ex)
            {
                return CommandResponse.Fail();
            }
        }
        private CommandResponse UpdateInternal(MinimoDatabase db, Project found, Project project)
        {
            found.Import(project);
            try
            {
                db.SaveChanges();
                return CommandResponse.Ok().AddKey(project.Id.ToString());
            }
            catch
            {
                return CommandResponse.Fail();
            }
        }
        public CommandResponse Save(Project project)
        {
            if (project == null || project.Id < 0)
                return CommandResponse.Fail().AddMessage(AppMessages.Err_UserNotFound);
            using var db = new MinimoDatabase();
            var found = (from u in db.Projects
                         where u.Id == project.Id && !u.Deleted
                         select u).SingleOrDefault();

            return found == null
                    ? CreateInternal(db, project)
                    : UpdateInternal(db, found, project);
        }
        public CommandResponse Delete(long Id)
        {
            using var db = new MinimoDatabase();
            var found = db.Projects.FirstOrDefault(x => x.Id == x.Id);
            if (found == null)
                return CommandResponse.Fail();
            try
            {
                db.Projects.Remove(found);
                db.SaveChanges();
                return CommandResponse.Ok();
            }
            catch (Exception ex)
            {
                return CommandResponse.Fail();
            }
        }
        public CommandResponse SoftDelete(long id)
        {
            using var db = new MinimoDatabase();
            var found = db.Projects.SingleOrDefault(x => x.Id == id);
            if (found == null)
                return CommandResponse.Fail();
            try
            {
                found.SoftDelete();
                db.SaveChanges();
                return CommandResponse.Ok();
            }
            catch (Exception ex)
            {
                return CommandResponse.Fail();
            }
        }
        private static string SaveToBlobStorage(BlobContainerClient container, Stream data, string name)
        {
            var blob = container.GetBlobClient(name);
            blob.DeleteIfExists();
            blob.Upload(data);
            return blob.Uri.AbsoluteUri;
        }

    }
}
