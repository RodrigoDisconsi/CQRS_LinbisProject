using FluentValidation;

namespace CQRSLinbis.Application.Projects.Commands.AddDeveloperToProject
{
    public class AddDeveloperToProjectValidator : AbstractValidator<AddDeveloperToProjectCommand>
    {
        public AddDeveloperToProjectValidator()
        {
            RuleFor(x => x.ProjectId)
                .NotEmpty()
                .WithMessage("ProjectId is required.");

            When(x => x.DeveloperId > 0, () =>
            {
                RuleFor(v => v.Name).NotEmpty().WithMessage("Name is required");
                RuleFor(v => v.CostByDay).NotEmpty().WithMessage("CostByDay is required.");
                RuleFor(v => v.AddedDate).NotEmpty().WithMessage("AddedDate is required.");
            });
        }
    }
}
