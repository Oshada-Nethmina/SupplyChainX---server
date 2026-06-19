using Microsoft.AspNetCore.Identity;
using SupplyChainX.Models;

namespace SupplyChainX.Helpers;

public class HashPasswordHelper
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public HashPasswordHelper(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public string HashPassword(User user, string password)
    {
        return _passwordHasher.HashPassword(user, password);
    }

    public bool VerifyPassword(User user, string hashedPassword, string password)
    {
        var result = _passwordHasher.VerifyHashedPassword(
            user,
            hashedPassword,
            password);

        return result != PasswordVerificationResult.Failed;
    }
}