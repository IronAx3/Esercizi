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

namespace Youbiquitous.Minimo.Persistence;

/// <summary>
/// Database console
/// </summary>
public partial class MinimoDatabase 
{
    /// <summary>
    /// Overridable method
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString, builder =>
        {
             builder.EnableRetryOnFailure(6, TimeSpan.FromSeconds(10), null);
        });
        base.OnConfiguring(optionsBuilder);
    }

    /// <summary>
    /// Overridable model builder
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ////////////////////////////////////////////////////////////
        //   Users
        // 
        modelBuilder.Entity<UserAccount>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<UserAccount>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
     

        modelBuilder.Entity<UserAccount>(p =>
        {
            p.ComplexProperty(u => u.ChangePassword).IsRequired();
            p.ComplexProperty(u => u.Confirmation).IsRequired();
            p.ComplexProperty(u => u.Mfa).IsRequired();
        });

        modelBuilder.Entity<UserAccount>()
           .HasOne(u => u.RoleInfo)
           .WithMany()
           .HasForeignKey(p => p.RoleId)
           .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<UserAccount>()
           .OwnsOne(r => r.TimeStamp);

        ////////////////////////////////////////////////////////////
        //  User Roles
        // 
        modelBuilder.Entity<Role>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<Role>()
           .OwnsOne(r => r.TimeStamp);
        modelBuilder.Entity<Role>()
           .Property(p => p.Id)
           .ValueGeneratedNever();

        ////////////////////////////////////////////////////////////
        //   Tenants
        // 
        modelBuilder.Entity<Tenant>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<Tenant>()
           .Property(p => p.Id)
           .ValueGeneratedNever();

        modelBuilder.Entity<Tenant>()
            .OwnsOne(r => r.TimeStamp);


        //////////////////////////////////////////////////////////////
        // Configurazione della relazione uno-a-molti tra Project e Tenant
        modelBuilder.Entity<Project>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
         modelBuilder.Entity<Project>()
            .HasOne(p => p.Tenant)
            .WithMany()
            .HasForeignKey(p => p.TenantId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Project>()
            .OwnsOne(p => p.TimeStamp);

        ////////////////////////////////////////////////////////////
        //   System-Updates
        // 
        modelBuilder.Entity<SystemUpdate>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<SystemUpdate>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        ////////////////////////////////////////////////////////////
        //  User Projects

        modelBuilder.Entity<UserProjectBinding>()
          .HasKey(upb => new { upb.UserId, upb.ProjectId});

        modelBuilder.Entity<UserProjectBinding>()
            .HasOne(upb => upb.User)
            .WithMany(u => u.UserProjectBindings)
            .HasForeignKey(upb => upb.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<UserProjectBinding>()
            .HasOne(upb => upb.Project)
            .WithMany(p => p.UserProjectBindings)
            .HasForeignKey(upb => upb.ProjectId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<UserProjectBinding>()
             .OwnsOne(p => p.TimeStamp);


        ////////////////////////////////////////////////////////////
        // Work
        modelBuilder.Entity<Work>()
            .HasKey(w => w.Id);
        modelBuilder.Entity<Work>()
            .Property(w => w.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Work>()
            .HasOne(w => w.User)
            .WithMany(w => w.Works)
            .HasForeignKey(w => w.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Work>()
            .HasOne(w => w.Project)
            .WithMany(w => w.Works)
            .HasForeignKey(w => w.ProjectId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Work>()
            .OwnsOne(p => p.TimeStamp);



    }
}