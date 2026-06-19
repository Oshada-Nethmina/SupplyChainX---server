using SupplyChainX.DTOs.Auth;

namespace SupplyChainX.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync (RegisterRequestDto registerRequest);
    Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequest);
    Task VerifyEmailAsync(string token);

    Task ForgotPasswordAsync(string email);

    Task ResetPasswordAsync(ResetPasswordRequestDto resetPasswordRequest);
}