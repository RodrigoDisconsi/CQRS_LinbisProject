using Controllers;
using CQRSLinbis.Application.Projects.Queries.GetProjects;
using CQRSLinbis.Application.Projects.Queries.Response;
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
    }
}
