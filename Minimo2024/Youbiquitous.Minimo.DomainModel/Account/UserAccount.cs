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
using System.Net;
using System.Text.Json.Serialization;
using Youbiquitous.Martlet.Core.Types.Locale;
using Youbiquitous.Martlet.Core.Types;

namespace Youbiquitous.Minimo.DomainModel.Account;

public partial class UserAccount : MinimoBaseEntity
{
    public UserAccount()
    {
        Confirmation = new AccountConfirmation();
        ChangePassword = new ChangePassword();
        Mfa = new AccountMfa();
    }

    public UserAccount(long tenantId, string email, int role)
    {
        TenantId = tenantId != 0 ? tenantId : Tenant.DefaultTenantId1;
        RoleId = Role.Validate(role);
        Email = email;
        LastName = Role.NameByDefaultRoleId(role);

        Confirmation = new AccountConfirmation();
        ChangePassword = new ChangePassword();
        Mfa = new AccountMfa();
    }

    public UserAccount(long tenantId, string email, int role, string firstName, string lastName)
    {
        TenantId = tenantId != 0 ? tenantId : Tenant.DefaultTenantId1;
        RoleId = Role.Validate(role);
        Email = email;
        LastName = lastName;
        FirstName = firstName;

        Confirmation = new AccountConfirmation();
        ChangePassword = new ChangePassword();
        Mfa = new AccountMfa();
    }

    #region User Properties
    /// <summary>
    /// Primary key
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// First name (optional)
    /// </summary>
    [MaxLength(60)]
    public string FirstName { get; set; }

    /// <summary>
    /// Last name (optional)
    /// </summary>
    [MaxLength(60)]
    public string LastName { get; set; }

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

    [MaxLength(500)]
    public string SerializedPermissions { get; set; }
    /// <summary>
    /// Additional optional information
    /// </summary>
    [MaxLength(60)]
    public string Extra { get; set; }

    public string Icon { get; set; }
    #endregion

    #region ForeignKeys
    /// <summary>
    /// Linked list of user projects
    /// </summary>
    [JsonIgnore]
    public List<UserProjectBinding> UserProjectBindings { get; set; }

    /// <summary>
    ///   Linked list of projects
    /// </summary>
    [JsonIgnore]
    public List<Project> Projects => UserProjectBindings?.Select(up => up.Project).ToList();

	[JsonIgnore]
    public List<Work> Works { get; set; }

    /// <summary>
    /// Linked list of user roles
    /// </summary>
    /// 
    public long RoleId { get; set; }
    public Role RoleInfo { get; set; }

    /// <summary>
    /// Reference to the corresponding tenant
    /// </summary>
    [MaxLength(30)]
    public long TenantId { get; set; }
    public Tenant Tenant { get; set; }
    #endregion

    public void Import(UserAccount user)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Email = user.Email;
        PasswordResetRequest = user.PasswordResetRequest;
        LastLogin = user.LastLogin;
        PasswordResetToken = user.PasswordResetToken;
        Password = user.Password;
        Extra = user.Extra;
        Icon = user.Icon;
        RoleId = user.RoleId;
        RoleInfo = user.RoleInfo;
        TenantId = user.TenantId;
        Tenant = user.Tenant;
        UserProjectBindings = user.UserProjectBindings;
    }

}
