using Domain.Extensions;
using FluentValidation;

namespace Domain.Models.Account.Registration;

public class RegisterModelValidation : AbstractValidator<RegisterModel>
{
    public RegisterModelValidation()
    {
        RuleFor(x => x.Login).NotEmpty();
        RuleFor(x => x.Email).ApplyEmailRules();

        RuleFor(x => x.Password).ApplyPasswordRules();

        RuleFor(x => x.ConfirmPassword)
            .ApplyPasswordRules()
            .Equal(x => x.Password).WithMessage("Passwords do not match.");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<RegisterModel>.CreateWithOptions((RegisterModel)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}