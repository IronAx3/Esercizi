using Logico.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youbiquitous.Minimo.DomainModel.Account
{
    public class UserProjectBinding : MinimoBaseEntity
    {
        public long UserId { get; set; }
        public UserAccount User { get; set; }

        public long ProjectId { get; set; }
        public Project Project { get; set; }

    }
}
