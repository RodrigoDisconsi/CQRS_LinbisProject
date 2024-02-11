using CQRSLinbis.Application.Common.Models;
using CQRSLinbis.Application.Projects.Commands.AddDeveloperToProject;
using CQRSLinbis.Application.Projects.Queries.GetProjectById;
using CQRSLinbis.Application.Projects.Queries.GetProjects;
using CQRSLinbis.Domain.Queries;

namespace CQRSLinbis.Application.Common.Interfaces.Services
{
    public interface IProjectService
    {
        Task<PaginatedList<ProjectView>> GetProjects(GetProjectsQuery query);
        Task<ProjectView> GetProjectById(GetProjectByIdQuery request);

        Task AddDeveloperToProjectAsync(AddDeveloperToProjectCommand request);
        Task DeleteProjectAsync(int projectId);
    }
}
