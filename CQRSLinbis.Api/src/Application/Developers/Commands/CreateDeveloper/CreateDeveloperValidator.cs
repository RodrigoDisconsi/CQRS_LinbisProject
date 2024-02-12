using FluentValidation;

namespace CQRSLinbis.Application.Developers.Commands.CreateDeveloper
{
    public class CreateDeveloperValidator : AbstractValidator<CreateDeveloperCommand>
    {
        public CreateDeveloperValidator()
        {
            RuleFor(v => v.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(v => v.CostByDay).NotEmpty().WithMessage("CostByDay is required.");
            RuleFor(v => v.AddedDate).NotEmpty().WithMessage("AddedDate is required.");
            RuleFor(v => v.IsActive).NotEmpty().WithMessage("IsActive is required.");
        }
    }
}
