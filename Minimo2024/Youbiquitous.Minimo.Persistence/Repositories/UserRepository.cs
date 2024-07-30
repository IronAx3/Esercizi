///////////////////////////////////////////////////////////////////
//
// Project MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

using Microsoft.EntityFrameworkCore;
using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Martlet.Services.Security.Password;
using Youbiquitous.Minimo.DomainModel.Account;
using Youbiquitous.Minimo.Shared.Extensions;

namespace Youbiquitous.Minimo.Persistence.Repositories;

public partial class UserRepository                           
{
    public static string BlobConnectionString { get; set; }

    public UserRepository(string blobConnStr)
    {
        BlobConnectionString = blobConnStr;
    }
    /// <summary>
    /// Find the role object by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public UserAccount FindById(long id)
    {
        using var db = new MinimoDatabase();
        var user = (from c in db.Users
                    where c.Id == id
                    select c).SingleOrDefault();
        return user;
    }

    /// <summary>
    /// Retrieve user
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static UserAccount FindByEmail(string email)
    {
        using var db = new MinimoDatabase();
        var user = (from u in db.Users
                .Include(u => u.RoleInfo)
                    where u.Email == email
                    select u).SingleOrDefault();
        return user;
    }


    /// <summary>
    /// Physical loader of records from the table to be held in memory
    /// </summary>
    /// <returns></returns>
    public IList<UserAccount> All()
    {
        using var db = new MinimoDatabase();
        var records = (from p in db.Users.Include(u => u.RoleInfo)
                       where !p.Deleted
                       select p).ToList();
        return records;
    }




}