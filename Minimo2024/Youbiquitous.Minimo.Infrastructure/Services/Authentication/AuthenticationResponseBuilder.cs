///////////////////////////////////////////////////////////////////
//
// Project MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//


using Youbiquitous.Minimo.DomainModel.Account;
using Youbiquitous.Minimo.Shared.Auth;

namespace Youbiquitous.Minimo.Infrastructure.Services.Authentication;

/// <summary>
/// Builds a valid authentication response
/// </summary>
public class AuthenticationResponseBuilder
{
    /// <summary>
    /// Builds an object from a UserLogin
    /// </summary>
    /// <param name="candidate"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static AuthenticationResponse BuildFrom(UserAccount candidate, string name)
    {
        var response = AuthenticationResponse.Ok()
            .AddSignature(name)
            .AddLoginId(candidate.Id)
            .AddEmail(candidate.Email)
            .AddDisplayName($"{candidate}")
            .AddRole(candidate.RoleId);

        return response;
    }
}