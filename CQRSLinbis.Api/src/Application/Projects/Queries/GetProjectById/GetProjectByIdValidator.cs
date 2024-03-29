﻿using FluentValidation;

namespace CQRSLinbis.Application.Projects.Queries.GetProjectById
{
    public class GetProjectByIdValidator : AbstractValidator<GetProjectByIdQuery>
    {
        public GetProjectByIdValidator()
        {
            RuleFor(x => x.ProjectId)
                .NotEmpty()
                .WithMessage("ProjectId is required.");
        }
    }
}
