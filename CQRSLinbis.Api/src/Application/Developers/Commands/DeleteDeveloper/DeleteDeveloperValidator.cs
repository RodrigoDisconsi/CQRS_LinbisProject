using CQRSLinbis.Application.Projects.Commands.DeleteProject;
using FluentValidation;

namespace CQRSLinbis.Application.Developers.Commands.DeleteDeveloper
{
    public class DeleteDeveloperValidator : AbstractValidator<DeleteDeveloperCommand>
    {
        public DeleteDeveloperValidator()
        {
            RuleFor(x => x.DeveloperId)
                .NotEmpty()
                .WithMessage("DeveloperId is required.");
        }
    }
}
