using Controllers;
using CQRSLinbis.Application.Projects.Commands.AddDeveloperToProject;
using CQRSLinbis.Application.Projects.Queries.GetProjects;
using CQRSLinbis.Application.Projects.Queries.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProjectController : ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProjectsResponse))]
        public async Task<ActionResult<GetProjectsResponse>> GetPermissions([FromQuery] GetProjectsQuery request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost("{projectId}/developers")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Unit>> AddDeveloperToProject(int projectId, AddDeveloperToProjectCommand request)
        {
            request.ProjectId = projectId;
            
            await Mediator.Send(request);

            return NoContent();
        }
    }
}
