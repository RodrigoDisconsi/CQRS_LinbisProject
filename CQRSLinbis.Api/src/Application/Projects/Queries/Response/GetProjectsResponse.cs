using CQRSLinbis.Application.Common.Models;

namespace CQRSLinbis.Application.Projects.Queries.Response
{
    public class GetProjectsResponse
    {
        public PaginatedList<ProjectDto> Projects { get; set; }
    }
}
