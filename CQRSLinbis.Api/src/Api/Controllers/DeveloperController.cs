using Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CQRSLinbis.Application.Developers.Commands.CreateDeveloper;
using CQRSLinbis.Application.Developers.Queries.GetDeveloperById;
using CQRSLinbis.Application.Developers.Commands.DeleteDeveloper;
using CQRSLinbis.Application.Developers.Queries.GetDevelopers.Response;

namespace API.Controllers
{
    public class DeveloperController : ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetDevelopersResponse))]
        public async Task<ActionResult<GetDevelopersResponse>> GetDevelopers([FromQuery]GetDevelopersQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Unit>> CreateDeveloper(CreateDeveloperCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{developerId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Unit>> DeleteDeveloper(int developerId)
        {
            await Mediator.Send(new DeleteDeveloperCommand { DeveloperId = developerId });

            return NoContent();
        }
    }
}
