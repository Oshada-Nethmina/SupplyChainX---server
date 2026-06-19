using SupplyChainX.Data;
using SupplyChainX.DTOs.Auth;
using SupplyChainX.Helpers;
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

    public Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerRequest)
    {
        throw new NotImplementedException();
    }

    public Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequest)
    {
        throw new NotImplementedException();
    }

    public Task VerifyEmailAsync(string token)
    {
        throw new NotImplementedException();
    }

    public Task ForgotPasswordAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task ResetPasswordAsync(ResetPasswordRequestDto resetPasswordRequest)
    {
        throw new NotImplementedException();
    }
}