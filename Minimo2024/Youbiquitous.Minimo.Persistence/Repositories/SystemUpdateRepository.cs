///////////////////////////////////////////////////////////////////
//
// Project MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

using Youbiquitous.Minimo.DomainModel.Audit;

namespace Youbiquitous.Minimo.Persistence.Repositories;

public static class SystemUpdateRepository
{
    /// <summary>
    /// Record an update to some parts of the system
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public static SystemUpdate Latest(string action)
    {
        using var db = new MinimoDatabase();
        var latest = db.SystemUpdates
            .OrderByDescending(u => u.When)
            .FirstOrDefault(u => u.Action == action);

        return latest ?? SystemUpdate.Empty();
    }

    /// <summary>
    /// Record an update to some parts of the system
    /// </summary>
    /// <param name="email"></param>
    /// <param name="action"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    public static void Mark(string email, string action, string description = null)
    {
        using var db = new MinimoDatabase();
        db.SystemUpdates.Add(SystemUpdate.New(email, action, description));
        db.SaveChanges();
    }


}