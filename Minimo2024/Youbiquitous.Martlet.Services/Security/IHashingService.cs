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
/// Public interface for a general-purpose hashing service
/// </summary>
public interface IHashingService
{
    bool Validate(string clearPassword, string hashedPassword);
    string Hash(string clearPassword);
}