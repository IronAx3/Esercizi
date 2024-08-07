﻿///////////////////////////////////////////////////////////////////
//
// Youbiquitous MARTLET Services 
// In-house cross-cutting services
// 2024
//
// Author: Youbiquitous Team
//


// ***************************************************************
//
//  Each password has its own salt of 512 chars
//  The salt is stored with the hashed password in the DB
//  Salt and password are interspersed 
//  Optional random padding is attached at the beginning of the stored string
//
// ***************************************************************

// SECURITY

using System.Security.Cryptography;

namespace Youbiquitous.Martlet.Services.Security.Password.Hashing;

/// <summary>
/// Implements a basic, canonical but sufficient behavior for hashing passwords
/// </summary>
public partial class DefaultPasswordHashingService : IHashingService
{
    /// <summary>
    /// Sets parameters for password hashing
    /// </summary>
    /// <param name="iterations"></param>
    /// <param name="paddingSize"></param>
    /// <returns></returns>
    public DefaultPasswordHashingService Config(int iterations, int paddingSize)
    {
        IterationCount = iterations <= MinIterationCount ? MinIterationCount : iterations;
        PaddingSize = paddingSize <= MinPaddingSize ? MinPaddingSize : paddingSize;  
        return this;
    }

    /// <summary>
    /// Hash the clear password and compare to the hashed one
    /// </summary>
    /// <param name="clearPassword"></param>
    /// <param name="hashedPassword"></param>
    /// <returns></returns>
    public bool Validate(string clearPassword, string hashedPassword)
    {
        // Extract the salt to use from hashed password
        var pp = DecomposeParts(hashedPassword);

        // Hash the provided password to see if it matches 
        var pbkdf2 = new Rfc2898DeriveBytes(clearPassword, pp.Salt, IterationCount, HashAlgorithmName.SHA512);
        var hash = pbkdf2.GetBytes(DefaultHashSize);

        // Make byte per byte comparison
        for (var i = 0; i < HashSize; i++)
        {
            if (pp.Content[i] != hash[i])
                return false;
        }

        return true;
    }

    /// <summary>
    /// Public wrapper for building the hash for the provided clear password
    /// </summary>
    /// <param name="clearPassword"></param>
    /// <returns></returns>
    public string Hash(string clearPassword)
    {
        return HashInternal(clearPassword);
    }
}