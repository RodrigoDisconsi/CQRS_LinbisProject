using CQRSLinbis.Application.Common.Interfaces.Services;
using MediatR;

namespace CQRSLinbis.Application.Projects.Commands.AddDeveloperToProject
{
    public class AddDeveloperToProjectCommand : IRequest
    {
        public int ProjectId { get; set; }
        public int DeveloperId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int CostByDay { get; set; }
        public DateTimeOffset AddedDate { get; set; }
    }

    public class AddDeveloperToProjectCommandHandler : IRequestHandler<AddDeveloperToProjectCommand>
    {
        private readonly IProjectService _projectService;

        public AddDeveloperToProjectCommandHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }
        public async Task<Unit> Handle(AddDeveloperToProjectCommand request, CancellationToken cancellationToken)
        {
            await _projectService.AddDeveloperToProjectAsync(request);

            return Unit.Value;
        }
    }

}