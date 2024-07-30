///////////////////////////////////////////////////////////////////
//
// Youbiquitous MARTLET Services 
// In-house cross-cutting services
// 2024
//
// Author: Youbiquitous Team
//


// ***************************************************************
//
//  Each password has its own salt 
//  The salt is stored with the hashed password in the DB
//  Salt and password are interspersed with optional padding
//  Optional random padding is attached at the beginning of the stored string
//
// ***************************************************************

// SECURITY

namespace Youbiquitous.Martlet.Services.Security.Password.Hashing;

public partial class DefaultPasswordHashingService : IHashingService
{
    protected const int DefaultSaltSize = 512;
    protected const int DefaultHashSize = 512;
    protected const int MinIterationCount = 1000;
    protected const int MinPaddingSize = 16;

    public DefaultPasswordHashingService()
    {
        IterationCount = 10000;
        PaddingSize = 32;
    }

    protected int SaltSize => DefaultSaltSize;
    protected int HashSize => DefaultHashSize;

    protected int IterationCount { get; set; }
    protected int PaddingSize { get; set; }
}