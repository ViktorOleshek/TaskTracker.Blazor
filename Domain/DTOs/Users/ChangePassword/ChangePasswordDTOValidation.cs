using Domain.Extensions;
using FluentValidation;

namespace Domain.DTOs.Users.ChangePassword;

public class ChangePasswordDTOValidation : AbstractValidator<ChangePasswordDTO>
{
    public ChangePasswordDTOValidation()
    {
        RuleFor(x => x.OldPassword).ApplyPasswordRules();
        RuleFor(x => x.NewPassword).ApplyPasswordRules();
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<ChangePasswordDTO>.CreateWithOptions((ChangePasswordDTO)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}