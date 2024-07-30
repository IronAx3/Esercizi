///////////////////////////////////////////////////////////////////
//
// Project MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

using Youbiquitous.Minimo.DomainModel.Account;
using Youbiquitous.Minimo.Persistence;

namespace Youbiquitous.Minimo.Infrastructure.Services.Authentication;

public static class AuthRepository
{
    /// <summary>
    /// Retrieve user
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static UserAccount FindByEmail(string email)
    {
        using var db = new MinimoDatabase();
        var user = (from u in db.Users
            where u.Email == email
            select u).SingleOrDefault();
        return user;
    }
}