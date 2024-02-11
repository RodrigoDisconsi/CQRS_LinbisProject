using AutoMapper;
using CQRSLinbis.Application.Common.Interfaces.Services;
using CQRSLinbis.Application.Projects.Queries.GetProjectById.Response;
using CQRSLinbis.Application.Projects.Queries.Models;
using MediatR;

namespace CQRSLinbis.Application.Projects.Queries.GetProjectById
{
    public class GetProjectByIdQuery : IRequest<GetProjectByIdResponse>
    {
        public int ProjectId{ get; set; }
    }

    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, GetProjectByIdResponse>
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        public GetProjectByIdQueryHandler(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        public async Task<GetProjectByIdResponse> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new GetProjectByIdResponse
            {
                Project = _mapper.Map<ProjectDto>(await _projectService.GetProjectById(request)),
            };

            return await Task.FromResult(response);
        }
    }


}