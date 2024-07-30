///////////////////////////////////////////////////////////////////
//
// Project MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//


using Microsoft.EntityFrameworkCore;
using Youbiquitous.Minimo.DomainModel.Account;
using Youbiquitous.Minimo.DomainModel.Audit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Youbiquitous.Minimo.Persistence;

/// <summary>
/// Database console
/// </summary>
public partial class MinimoDatabase : DbContext
{
    public static string ConnectionString = "";

    /// <summary>
    /// Tables
    /// </summary>
    public DbSet<Tenant> Tenants { get; set; }  
    public DbSet<Work> Works { get; set; }
    public DbSet<UserAccount> Users { get; set; }  
    public DbSet<Project> Projects { get; set; }
    public DbSet<Tenant> Companies { get; set; }
    public DbSet<UserProjectBinding> UserProjectBindings { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<SystemUpdate> SystemUpdates { get; set; }


    /// <summary>
    /// Clear all local cache(s)
    /// </summary>
    public static void InvalidateCache()
    {
    }
}