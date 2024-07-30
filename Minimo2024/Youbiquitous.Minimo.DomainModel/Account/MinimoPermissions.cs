///////////////////////////////////////////////////////////////////
//
// Project MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

using System.Text;
using Youbiquitous.Martlet.Core.Extensions;

namespace Youbiquitous.Minimo.DomainModel.Account;

public class MinimoPermissions
{
    public const string Tennis = "T";
    public const string Padel = "P";
    private const char Sep = ',';

    public MinimoPermissions(bool t = false, bool p = false)
    {
        SupportTennis = t;
        SupportPadel = p;
    }

    /// <summary>
    /// Build a permission object from a string
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static MinimoPermissions Parse(string input)
    {
        if (input.IsNullOrWhitespace())
            return new MinimoPermissions(true, true);

        var permissions = new MinimoPermissions();
        var tokens = input.Split(Sep);
        if (tokens.Contains(Tennis))
            permissions.SupportTennis = true;
        if (tokens.Contains(Padel))
            permissions.SupportPadel = true;

        return permissions;
    }

    /// <summary>
    /// Whether access to tennis module is permitted
    /// </summary>
    public bool SupportTennis { get; private set; }

    /// <summary>
    /// Whether access to padel module is permitted
    /// </summary>
    public bool SupportPadel { get; private set; }

    /// <summary>
    /// Whether no permissions are granted
    /// </summary>
    /// <returns></returns>
    public bool None()
    {
        return !SupportTennis && !SupportPadel;
    }

    /// <summary>
    /// Whether all permissions are granted
    /// </summary>
    /// <returns></returns>
    public bool All()
    {
        return SupportTennis && SupportPadel;
    }

    /// <summary>
    /// Whether current object has all expected permissions
    /// </summary>
    /// <param name="permissions"></param>
    /// <returns></returns>
    public bool MatchWith(string permissions)
    {
        var expected = Parse(permissions);
        return MatchWith(expected);
    }

    /// <summary>
    /// Whether current object has all expected permissions
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public bool MatchWith(string[] list)
    {
        var permissions = string.Join(Sep, list);
        var expected = Parse(permissions);
        return MatchWith(expected);
    }
    /// <summary>
    /// Whether current object has all expected permissions
    /// </summary>
    /// <param name="expected"></param>
    /// <returns></returns>
    public bool MatchWith(MinimoPermissions expected)
    {
        if (expected.SupportPadel && !SupportPadel)
            return false;
        if (expected.SupportTennis && !SupportTennis)
            return false;
        return true;
    }

    /// <summary>
    /// Serializes current permissions 
    /// </summary>
    /// <returns></returns>
    public string Serialize()
    {
        var builder = new StringBuilder();
        if (SupportTennis) 
            builder.AppendFormat("{0},", Tennis);
        if (SupportPadel) 
            builder.AppendFormat("{0},", Padel);

        var output = builder.ToString().Trim(Sep);
        return output.IsNullOrWhitespace() ? null : output;
    }
}