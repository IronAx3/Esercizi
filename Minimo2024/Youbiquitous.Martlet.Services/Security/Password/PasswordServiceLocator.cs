///////////////////////////////////////////////////////////////////
//
// Youbiquitous MARTLET Services 
// In-house cross-cutting services
// 2024
//
// Author: Youbiquitous Team
//



using Youbiquitous.Martlet.Services.Security.Password.Hashing;

// SECURITY
namespace Youbiquitous.Martlet.Services.Security.Password;

/// <summary>
/// Returns the instance to use
/// </summary>
public static class PasswordServiceLocator
{
    public static IPasswordService Get()
    {
        return new DefaultPasswordService(new DefaultPasswordHashingService());
    }
}