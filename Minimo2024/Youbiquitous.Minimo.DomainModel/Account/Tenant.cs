///////////////////////////////////////////////////////////////////
//
// Project MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

using Logico.DomainModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace Youbiquitous.Minimo.DomainModel.Account;

public class Tenant : MinimoBaseEntity
{

    public const long DefaultTenantId1 = 1;
    public const long DefaultTenantId2 = 2;
    public const long DefaultTenantId3 = 3;

    public const string DefaultTenantId1Name = "Crionet";
    public const string DefaultTenantId2Name = "Eyeson";
    public const string DefaultTenantId3Name = "Ligr";

    public Tenant()
    {
        RelatedUsers = new List<UserAccount>();
    }

    public Tenant(long id, string logoUrl, string permissions = null) : this()
    {
        Id = id;
        DisplayName = id.ToString();
        Permissions = permissions;
        LogoUrl = logoUrl;
    }

    public static Tenant Create(long id, string logoUrl, string permissions = null) => new Tenant(id, logoUrl, permissions);

    /// <summary>
    /// Unique identifier of the tenant (primary key)
    /// </summary>
    public long Id { get; private set; }

    public string Name { get;set; }

    /// <summary>
    /// Display name
    /// </summary>
    [MaxLength(50)]
    public string DisplayName { get; set; }

    /// <summary>
    /// Text defining permissions assigned to the tenant
    /// </summary>
    [MaxLength(40)]
    public string Permissions { get; private set; }

	[MaxLength(50)]
	public string Email { get; set; }
	public bool EmailConfirmed { get; set; }
	/// <summary>
	/// Password
	/// </summary>
	[MaxLength(1500)]
	public string Password { get; set; }

	public DateTime? LastLogin { get; set; }
	public DateTime? PasswordResetRequest { get; set; }

	[MaxLength(500)]
	public string PasswordResetToken { get; set; }
	public DateTime? LatestPasswordChange { get; set; }

	/// <summary>
	/// Photo of the tenant (logo URL)
	/// </summary>
	[MaxLength(80)]
    public string LogoUrl { get; set; }

    /// <summary>
    /// Related users
    /// </summary>
    public IEnumerable<UserAccount> RelatedUsers { get; set; }


    /// <summary>
    /// Linked list of projects
    /// </summary>
    //public ICollection<Project> Projects { get; set; }

    /// <summary>
    /// Structured object representing permissions
    /// </summary>
    /// <returns></returns>
    public MinimoPermissions GetPermissions()
    {
        return MinimoPermissions.Parse(Permissions);
    }

    /// <summary>
    /// For display
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return DisplayName ?? Id.ToString();
    }

    public static string NameByDefaultRoleId(long tenantId)
    {
        return tenantId switch
        {
            DefaultTenantId1 => DefaultTenantId1Name,
            DefaultTenantId2 => DefaultTenantId2Name,
            DefaultTenantId3 => DefaultTenantId3Name,
            _ => null
        };
 
    }

    public void Import(Tenant tenant)
    {
        Name = tenant.Name;
        Email = tenant.Email;
        Password = tenant.Password;
        LastLogin = tenant.LastLogin;
        LatestPasswordChange = tenant.LatestPasswordChange;
        LogoUrl = tenant.LogoUrl;

    }



}