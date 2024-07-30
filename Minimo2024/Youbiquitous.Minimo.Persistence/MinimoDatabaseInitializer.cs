///////////////////////////////////////////////////////////////////
//
// Project MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//


using Azure;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Youbiquitous.Martlet.Services.Security.Password;
using Youbiquitous.Minimo.DomainModel.Account;

namespace Youbiquitous.Minimo.Persistence;

/// <summary>
/// Database console
/// </summary>
public static class MinimoDatabaseInitializer
{
    /// <summary>
    /// Database initializer
    /// </summary>
    /// <param name="connString"></param>
    public static void Initialize(string connString)
    {
        MinimoDatabase.ConnectionString = connString;

        var db1 = new MinimoDatabase();
        if (db1.Database.EnsureCreated())
            Seed(db1);
    }

    /// <summary>
    /// Pre-populate the database upon system startup
    /// </summary>
    /// <param name="context"></param>
    private static void Seed(MinimoDatabase context)
    {
        AddDefaultTenants(context);
        AddDefaultRoles(context);
        AddDefaultUsers(context);
        AddDefaultProjects(context);
    }

    private static void AddDefaultRoles(MinimoDatabase context)
    {
        if (!context.Roles.Any())
        {
            List<Role> roles = new()
            {
              new Role{Id = Role.System, Name = Role.SystemName , DisplayName = Role.SystemName },
              new Role{Id = Role.Operator, Name = Role.OperatorName , DisplayName = Role.OperatorName },
              new Role{Id = Role.Admin, Name = Role.AdminName , DisplayName = Role.AdminName },
              new Role{Id = Role.Tenant, Name = Role.TenantName , DisplayName = Role.TenantName },
            };
            context.Roles.AddRange(roles);
            context.SaveChanges();
        }
    }
    private static void AddDefaultProjects(MinimoDatabase context)
    {
		if (!context.Projects.Any())
		{
			List<Project> projects = new()
			{
			    new Project{DisplayName = "Minimo", TenantId = 1},
                new Project{DisplayName = "Logico", TenantId = 1},
                new Project{DisplayName = "Uno", TenantId = 1},
                new Project{DisplayName = "TimeReport", TenantId = 1}

			};
			context.Projects.AddRange(projects);
			context.SaveChanges();
		}

	}

    private static void AddDefaultTenants(MinimoDatabase context)
    {
        // Tenants
        if (!context.Tenants.Any())
        {
            var ten1 = Tenant.Create(Tenant.DefaultTenantId1, $"/images/logos/{Tenant.DefaultTenantId1Name.ToLower()}-xs.png");
            var ten2 = Tenant.Create(Tenant.DefaultTenantId2, $"/images/logos/{Tenant.DefaultTenantId2Name.ToLower()}-xs.png");
            var ten3 = Tenant.Create(Tenant.DefaultTenantId3, $"/images/logos/{Tenant.DefaultTenantId3Name.ToLower()}-xs.png");

            context.Tenants.Add(ten1);
            context.Tenants.Add(ten2);
            context.Tenants.Add(ten3);
            context.SaveChanges();
        }
    }

    /// <summary>
    /// Initial feed of the database with default users/tenants
    /// </summary>
    /// <param name="context"></param>
    private static void AddDefaultUsers(MinimoDatabase context)
    {
        // Users
        if (!context.Users.Any())
        {
            var password = PasswordServiceLocator.Get();

            // Set numbering on user accounts
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Users', RESEED, 10001)");

            List<long> tenantIds = new()
            {
                Tenant.DefaultTenantId1,Tenant.DefaultTenantId2,Tenant.DefaultTenantId3
            };
            List<int> roles = new()
            {
                Role.Tenant,
                Role.Admin,
                Role.Operator,
            };
            Dictionary<int, string> roleIcons = new()
            {
                { Role.System ,  "fa-regular fa-gears" },
                { Role.Tenant, "fa-regular fa-chess-king" },
                { Role.Admin,  "fa-regular fa-user-tie" },
                { Role.Operator, "fa-regular fa-hammer" },
            };

            var sys = new UserAccount(Tenant.DefaultTenantId1, "system@crionet.com", Role.System)
            {
                Password = password.Store("LaM1aPa$$word_e_Diff876"),
                LastName = Role.SystemName,
                Icon = roleIcons[Role.System]
            };
            context.Users.Add(sys);

            // Add different users
            foreach (var tenantId in tenantIds)
            {
                foreach (var role in roles)
                {
                    var user = new UserAccount(tenantId, $"{role}@{Tenant.NameByDefaultRoleId(tenantId)}.com", role)
                    {
                        Password = password.Store("zaq12wsx"),
                        LastName = Role.NameByDefaultRoleId(role),
                        Icon = roleIcons[role]
                    };
                    context.Users.Add(user);
                }
            }
            context.SaveChanges();
        }

    }
}