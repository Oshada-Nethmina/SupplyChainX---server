using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using SupplyChainX.Data;
using SupplyChainX.DTOs.Auth;
using SupplyChainX.Exceptions;
using SupplyChainX.Helpers;
using SupplyChainX.Models;
using SupplyChainX.Services.Interfaces;

namespace SupplyChainX.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly SupplyChainDbContext _supplyChainDbContext;
    private readonly JwtTokenHelper _jwtTokenHelper;
    private readonly HashPasswordHelper _hashPasswordHelper;
    private readonly IEmailService _emailService;

    public AuthService(SupplyChainDbContext supplyChainDbContext, JwtTokenHelper jwtTokenHelper, HashPasswordHelper hashPasswordHelper, IEmailService emailService)
    {
        _supplyChainDbContext = supplyChainDbContext;
        _jwtTokenHelper = jwtTokenHelper;
        _hashPasswordHelper = hashPasswordHelper;
        _emailService = emailService;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerRequest)
    {
        if (await _supplyChainDbContext.Users.AnyAsync(x => x.Email == registerRequest.Email))
            throw new AuthException("Email already exists",409);

        var user = new User
        {
            FullName = registerRequest.FullName,
            Email = registerRequest.Email,
            RoleId = registerRequest.RoleId,
            IsEmailVerified = false,
            EmailVerificationToken =
                Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            EmailVerificationExpiry = DateTime.UtcNow.AddHours(24)
        };

        user.PasswordHash = _hashPasswordHelper.HashPassword(user, registerRequest.Password);

        _supplyChainDbContext.Users.Add(user);

        await _supplyChainDbContext.SaveChangesAsync();

        var verificationLink =
            $"http://localhost:3000/verify-email?token={user.EmailVerificationToken}";

        await _emailService.SendEmailAsync(
            user.Email,
            "Verify Email",
            $"Click <a href='{verificationLink}'>here</a> to verify your email.");

        return new AuthResponseDto
        {
            Token = _jwtTokenHelper.GenerateJwtToken(user),
            ExpiresAt = DateTime.UtcNow.AddHours(2)
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequest)
    {
        var user = await _supplyChainDbContext.Users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Email == loginRequest.Email);
        
        if (user == null)
            throw new AuthException("Email or password is incorrect", 401);

        if (!user.IsEmailVerified)
            throw new AuthException("Email or password is incorrect", 403);
       
        bool validPassword = _hashPasswordHelper.VerifyPassword(user, user.PasswordHash, loginRequest.Password);
        
        if (!validPassword)
            throw new AuthException("Password is incorrect", 403);

        return new AuthResponseDto
        {
            Token = _jwtTokenHelper.GenerateJwtToken(user),
            Role = user.Role.Name,
            ExpiresAt = DateTime.UtcNow.AddHours(2)
        };
    }

    public async Task VerifyEmailAsync(string token)
    {
       var user = await _supplyChainDbContext.Users
           .FirstOrDefaultAsync(x => x.EmailVerificationToken == token);
       
       if (user == null)
           throw new AuthException("Invalid token", 403);

       if (user.EmailVerificationExpiry < DateTime.UtcNow)
           throw new AuthException("Token expired", 403);
     
       user.IsEmailVerified = true;
       user.EmailVerificationToken = null;
       user.EmailVerificationExpiry = null;
       
       await _supplyChainDbContext.SaveChangesAsync();
    }

    public async Task ForgotPasswordAsync(string email)
    {
        var user = await _supplyChainDbContext.Users
            .FirstOrDefaultAsync(x => x.Email == email);

        if (user == null)
            return;

        user.PasswordResetToken =
            Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        user.PasswordResetExpiry =
            DateTime.UtcNow.AddHours(1);

        await _supplyChainDbContext.SaveChangesAsync();

        var resetLink =
            $"http://localhost:3000/reset-password?token={user.PasswordResetToken}";

        await _emailService.SendEmailAsync(
            user.Email,
            "Reset Password",
            $"Click <a href='{resetLink}'>here</a> to reset your password.");
    }

    public async Task ResetPasswordAsync(ResetPasswordRequestDto resetPasswordRequest)
    {
        var user = await _supplyChainDbContext.Users
            .FirstOrDefaultAsync(x =>
                x.PasswordResetToken == resetPasswordRequest.Token);
        
        if (user == null)
            throw new AuthException("Invalid token", 403);

        if (user.PasswordResetExpiry < DateTime.UtcNow)
            throw new AuthException("Token expired", 403);
        
        user.PasswordHash = _hashPasswordHelper.HashPassword(user, resetPasswordRequest.NewPassword);
        
        user.PasswordResetToken = null;
        user.PasswordResetExpiry = null;
        
        await _supplyChainDbContext.SaveChangesAsync();
    }
    
}