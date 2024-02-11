using CQRSLinbis.Application.Common.Models;
using CQRSLinbis.Application.Projects.Queries.Models;

namespace CQRSLinbis.Application.Projects.Queries.GetProjects.Response
{
    public class GetProjectsResponse
    {
        public PaginatedList<ProjectDto> Projects { get; set; }
    }
}
