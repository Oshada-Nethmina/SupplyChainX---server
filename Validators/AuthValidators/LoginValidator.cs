using FluentValidation;
using SupplyChainX.DTOs.Auth;

namespace SupplyChainX.Validators.AuthValidators;

public class LoginValidator : AbstractValidator<LoginRequestDto>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}