using Controllers;
using CQRSLinbis.Application.Projects.Commands.AddDeveloperToProject;
using CQRSLinbis.Application.Projects.Commands.DeleteProject;
using CQRSLinbis.Application.Projects.Queries.GetProjectById;
using CQRSLinbis.Application.Projects.Queries.GetProjectById.Response;
using CQRSLinbis.Application.Projects.Queries.GetProjects;
using CQRSLinbis.Application.Projects.Queries.GetProjects.Response;
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
        public async Task<ActionResult<GetProjectsResponse>> GetProjects([FromQuery] GetProjectsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{projectId}/developers")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProjectByIdResponse))]
        public async Task<ActionResult<GetProjectByIdResponse>> GetProjectById(int projectId)
        {
            var response = await Mediator.Send(new GetProjectByIdQuery { ProjectId = projectId });

            return Ok(response.Project);
        }

        [HttpPost("{projectId}/developers")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Unit>> AddDeveloperToProject(int projectId, AddDeveloperToProjectCommand command)
        {
            command.ProjectId = projectId;

            await Mediator.Send(command);

            return new ObjectResult(String.Empty) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpDelete("{projectId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Unit>> DeleteProject(int projectId)
        {
            await Mediator.Send(new DeleteProjectCommand { ProjectId = projectId });

            return NoContent();
        }
    }
}
