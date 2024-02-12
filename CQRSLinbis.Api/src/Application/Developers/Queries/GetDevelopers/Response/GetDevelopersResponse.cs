using CQRSLinbis.Application.Developers.Queries.Models;

namespace CQRSLinbis.Application.Developers.Queries.GetDevelopers.Response
{
    public class GetDevelopersResponse
    {
        public List<DeveloperDto> Developers {  get; set; }
    }
}
