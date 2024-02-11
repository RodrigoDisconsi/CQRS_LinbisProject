using FluentValidation;

namespace CQRSLinbis.Application.Projects.Commands.DeleteProject
{
    public class DeleteProjectValidator : AbstractValidator<DeleteProjectCommand>
    {
        public DeleteProjectValidator()
        {
            RuleFor(x => x.ProjectId)
                .NotEmpty()
                .WithMessage("ProjectId is required.");
        }
    }
}
