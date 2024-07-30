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
using System.Text.Json.Serialization;
using System.Xml.Linq;
using Youbiquitous.Martlet.Core.Extensions;

namespace Youbiquitous.Minimo.DomainModel.Account;

public class Role : MinimoBaseEntity
{
    public Role() { }

    public Role(long id, string name, string displayName)
    {
        Id = id;
        Name = name;
        DisplayName = displayName;
    }

    #region Supported Roles
    public const int System = 1;
    public const int Operator = 2;
    public const int Admin = 3;
    public const int Tenant = 4;

    public const string SystemName = "System";
    public const string OperatorName = "Operator";
    public const string AdminName = "Admin";
    public const string TenantName = "Tenant";
    #endregion
 
    /// <summary>
    /// String identifier of the role
    /// (This sample model supports System and Operator)
    /// </summary>
    [MaxLength(30)]
    public long Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }

    /// <summary>
    /// Validate the string as a valid role
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public static int Validate(int role)
    {
        if (role == null)
            return Operator;

        return role.IsAnyOf(System, Operator, Admin, Tenant) ? role : Operator;
    }

    /// <summary>
    /// Whether given string equates an Operator role
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public static bool IsOperator(int role)
    {
        if (role == null)
            return false;

        return role.IsAnyOf(Operator);
    }

    /// <summary>
    /// Whether given string equates a System role
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public static bool IsSystem(int role)
    {
        if (role == null)
            return false;

        return role.IsAnyOf(System);
    }

    /// <summary>
    /// Whether given string equates an Admin role
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public static bool IsAdmin(int role)
    {
        if (role == null)
            return false;

        return role.IsAnyOf(Admin);
    }

    /// <summary>
    /// Whether given string equates an Tenant role
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public static bool IsTenant(int role)
    {
        if (role == null)
            return false;

        return role.IsAnyOf(Tenant);
    }

    public static string NameByDefaultRoleId(int role)
    {
        return role switch
        {
            System => SystemName,
            Tenant => TenantName,
            Admin => OperatorName,
            Operator => OperatorName,
            _ => null
        };
    }


}