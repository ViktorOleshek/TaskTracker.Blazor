using FluentValidation;

namespace Domain.Models.Account;

public class RegisterModelValidation : AbstractValidator<RegisterModel>
{
    public RegisterModelValidation()
    {
        RuleFor(x => x.Login).NotEmpty();
        RuleFor(x => x.Email).NotEmpty();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(5).MaximumLength(10)
            .Matches(@"[A-Z]+").Matches(@"[a-z]+")
            .Matches(@"[0-9]+").Matches(@"[\@\!\?\.\*]+");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .Equal(x => x.Password)
            .WithMessage("Passwords do not match.");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<RegisterModel>.CreateWithOptions((RegisterModel)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}