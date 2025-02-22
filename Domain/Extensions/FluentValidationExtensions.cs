using FluentValidation;

namespace Domain.Extensions;

public static class FluentValidationExtensions
{
    public static IRuleBuilder<T, string> ApplyPasswordRules<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(5).WithMessage("Password must be at least 5 characters long.")
            .MaximumLength(30).WithMessage("Password must not exceed 30 characters.")
            .Matches(@"[A-Z]+").WithMessage("Password must contain at least one uppercase letter (A-Z).")
            .Matches(@"[a-z]+").WithMessage("Password must contain at least one lowercase letter (a-z).")
            .Matches(@"[0-9]+").WithMessage("Password must contain at least one number (0-9).")
            .Matches(@"[\@\!\?\.\*]+").WithMessage("Password must contain at least one special character (@, !, ?, ., *).");
    }

    public static IRuleBuilder<T, string> ApplyEmailRules<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .EmailAddress();
    }
}