using Domain.Extensions;
using FluentValidation;

namespace Domain.Models.Account.Login;

public class LoginModelValidation : AbstractValidator<LoginModel>
{
    public LoginModelValidation()
    {
        RuleFor(x => x.Login).NotEmpty();

        RuleFor(x => x.Password).ApplyPasswordRules();
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<LoginModel>.CreateWithOptions((LoginModel)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}