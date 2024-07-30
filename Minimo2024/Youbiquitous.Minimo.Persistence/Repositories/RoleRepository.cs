using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Youbiquitous.Minimo.DomainModel.Account;

namespace Youbiquitous.Minimo.Persistence.Repositories
{
    public class RoleRepository
    {
        public List<Role> All()
        {
            using var db = new MinimoDatabase();
            return db.Roles?.ToList();
        }
    }
}
