using FluentValidation;

namespace Domain.Models.Account;

public class LoginModelValidation : AbstractValidator<LoginModel>
{
    public LoginModelValidation()
    {
        RuleFor(x => x.Login).NotEmpty();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(5).MaximumLength(10)
            .Matches(@"[A-Z]+").Matches(@"[a-z]+")
            .Matches(@"[0-9]+").Matches(@"[\@\!\?\.\*]+");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<LoginModel>.CreateWithOptions((LoginModel)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}