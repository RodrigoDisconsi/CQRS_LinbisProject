using CQRSLinbis.Application.Common.Models;
using CQRSLinbis.Application.Developers.Commands.CreateDeveloper;
using CQRSLinbis.Application.Developers.Queries.GetDeveloperById;
using CQRSLinbis.Domain.Entities;
using CQRSLinbis.Domain.Queries;

namespace CQRSLinbis.Application.Common.Interfaces.Services
{
    public interface IDeveloperService
    {
        Task<Developer> GetDeveloperByIdAsync(int developerId);
        Task<PaginatedList<DeveloperView>> GetDevelopers(GetDevelopersQuery query);
        Task CreateDeveloper(CreateDeveloperCommand developer);
        Task DeleteDeveloperAsync(int developerId);
    }
}
