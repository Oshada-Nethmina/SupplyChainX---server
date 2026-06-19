using Microsoft.AspNetCore.Mvc;
using SupplyChainX.DTOs.Auth;
using SupplyChainX.Services.Interfaces;

namespace SupplyChainX.Controllers;

[ApiController]
[Route("(api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Login(LoginRequestDto loginRequest)
    {
        var result = await _authService.LoginAsync(loginRequest);
        return result is null
            ? Unauthorized(new { message = "Invalid email or password." })
            : Ok(result);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> RegisterAsync(RegisterRequestDto registerRequest)
    {
        var result = await _authService.RegisterAsync(registerRequest);
        return Ok(result);
        
    }
}