using CQRSLinbis.Application.Common.Interfaces.Services;
using MediatR;
using System.Text.Json.Serialization;

namespace CQRSLinbis.Application.Projects.Commands.AddDeveloperToProject
{
    public class AddDeveloperToProjectCommand : IRequest
    {
        [JsonIgnore]
        public int ProjectId { get; set; }
        public int DeveloperId { get; set; } = 0;
        public string Name { get; set; }
        public bool IsActive { get; set; } = false;
        public int CostByDay { get; set; } = 0;
        public long AddedDate { get; set; } = 0;
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