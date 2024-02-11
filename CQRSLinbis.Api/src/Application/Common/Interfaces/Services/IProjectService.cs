using CQRSLinbis.Application.Common.Models;
using CQRSLinbis.Application.Projects.Commands.AddDeveloperToProject;
using CQRSLinbis.Application.Projects.Queries.GetProjects;
using CQRSLinbis.Domain.Queries;

namespace CQRSLinbis.Application.Common.Interfaces.Services
{
    public interface IProjectService
    {
        Task<PaginatedList<ProjectView>> GetProjects(GetProjectsQuery query);

        Task AddDeveloperToProjectAsync(AddDeveloperToProjectCommand request);
    }
}
