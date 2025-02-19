using FluentValidation;

namespace Domain.Models.Project.Update;

public class ProjectModelValidation : AbstractValidator<ProjectModel>
{
    public ProjectModelValidation()
    {
        RuleFor(x => x.ProjectName).NotEmpty().WithMessage("Project name is required.");
        //RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start date is required.");
        //RuleFor(x => x.FinishDate)
        //    .GreaterThanOrEqualTo(x => x.StartDate)
        //    .When(x => x.FinishDate.HasValue)
        //    .WithMessage("Finish date cannot be earlier than Start date.");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<ProjectModel>
            .CreateWithOptions((ProjectModel)model, x => x.IncludeProperties(propertyName)));
        return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
    };
}
