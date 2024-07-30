///////////////////////////////////////////////////////////////////
//
// Youbiquitous MARTLET Services 
// In-house cross-cutting services
// 2024
//
// Author: Youbiquitous Team
//



// SECURITY
namespace Youbiquitous.Martlet.Services.Security;

/// <summary>
/// Public interface for password-related core functions
/// </summary>
public interface IPasswordService
{
    /// <summary>
    /// Ensures that hash of passed password matches the given hash
    /// </summary>
    /// <param name="clearPassword"></param>
    /// <param name="hashedPassword"></param>
    /// <returns></returns>
    bool Validate(string clearPassword, string hashedPassword);

    /// <summary>
    /// Returns the password string ready for storage (typically, a hash of the given string) 
    /// </summary>
    /// <param name="clearPassword"></param>
    /// <returns></returns>
    string Store(string clearPassword);

    /// <summary>
    /// Check if old and new passwords can be switched 
    /// </summary>
    /// <param name="storedHash"></param>
    /// <param name="oldPassword"></param>
    /// <param name="newPassword"></param>
    /// <returns></returns>
    bool CanChangePassword(string storedHash, string oldPassword, string newPassword);

    /// <summary>
    /// Checks whether the provided string is considered a strong password
    /// </summary>
    /// <param name="proposedPassword"></param>
    /// <returns></returns>
    bool IsStrongPassword(string proposedPassword);
}