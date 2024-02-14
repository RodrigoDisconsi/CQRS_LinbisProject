using AutoMapper;
using CQRSLinbis.Application.Common.Base;
using CQRSLinbis.Application.Common.Interfaces.Services;
using CQRSLinbis.Application.Projects.Queries.GetProjects.Response;
using CQRSLinbis.Application.Projects.Queries.Models;
using MediatR;

namespace CQRSLinbis.Application.Projects.Queries.GetProjects
{
    public class GetProjectsQuery : PagerBase, IRequest<GetProjectsResponse>
    {
        public string? SearchText { get; set; }
    }

    public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, GetProjectsResponse>
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        public GetProjectsQueryHandler(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        public async Task<GetProjectsResponse> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var response = new GetProjectsResponse
            {
                Projects = (await _projectService.GetProjects(request)).Map<ProjectDto>(_mapper.ConfigurationProvider),
            };

            return await Task.FromResult(response);
        }
    }


}